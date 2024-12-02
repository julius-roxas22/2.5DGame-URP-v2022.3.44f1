using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
    }

    public enum SceneBuilder
    {
        CharacterSelectScene,
        MainScene
    }

    public class CharacterControl : MonoBehaviour
    {

        [Header("Gravity")]
        public float GravityMultiplier;
        public float PullMultiplier;

        [Header("SubComponent")]
        public LedgeChecker ledgeChecker;
        public AnimationProgress AnimProgress;
        public AIProgress NPCAnimProgress;
        public DamageDetector damageDetector;
        public AIController NPCController;
        public BoxCollider PlayerBoxCollider;

        [Header("Input")]
        public bool Jump;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Attack;
        public bool MoveUp;
        public bool MoveDown;
        public bool Turbo;

        [Header("Setup")]
        public List<Collider> RagdollParts = new List<Collider>();
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public GameObject RightHandAttack;
        public GameObject LeftHandAttack;
        public CharacterColorType characterColorType;
        public float movementSpeed;
        public Animator skinnedMeshAnimator;
        public Material mat;

        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> ParentObjDictionaries = new Dictionary<string, GameObject>();
        private Rigidbody rigid;
        public ContactPoint[] ContactPoints;

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (null == rigid)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            AnimProgress = GetComponent<AnimationProgress>();
            NPCAnimProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponent<DamageDetector>();
            NPCController = GetComponentInChildren<AIController>();
            PlayerBoxCollider = GetComponent<BoxCollider>();

            SetUpSphereEdge();

            RegisterCharacter();
        }

        private void RegisterCharacter()
        {
            if (!CharacterManager.Instance.characters.Contains(this))
            {
                CharacterManager.Instance.characters.Add(this);
            }
        }

        public Collider FindTargetCameraLimb(string limbName)
        {
            foreach (Collider col in RagdollParts)
            {
                if (col.gameObject.name.Contains(limbName))
                {
                    return col;
                }
            }
            return null;
        }

        public GameObject GetChildObj(string names)
        {
            if (ParentObjDictionaries.ContainsKey(names))
            {
                return ParentObjDictionaries[names];
            }

            Transform[] arr = GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.gameObject.name.Equals(names))
                {
                    ParentObjDictionaries.Add(names, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }

        public void SetUpRagdollParts()
        {
            RagdollParts.Clear();
            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    if (null == col.gameObject.GetComponent<LedgeChecker>())
                    {
                        col.isTrigger = true;
                        RagdollParts.Add(col);
                        col.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        col.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                        CharacterJoint joint = col.gameObject.GetComponent<CharacterJoint>();
                        if (null != joint)
                        {
                            joint.enableProjection = true;
                        }

                        if (null == col.GetComponent<TriggerDetector>())
                        {
                            col.gameObject.AddComponent<TriggerDetector>();
                        }
                    }

                }
            }
        }

        public void TurnOnRagdoll()
        {

            Transform[] transforms = GetComponentsInChildren<Transform>();
            foreach (Transform t in transforms)
            {
                t.gameObject.layer = LayerMask.NameToLayer("DeadBody");
            }

            foreach (Collider col in RagdollParts)
            {
                TriggerDetector det = col.GetComponent<TriggerDetector>();
                det.LastLocalPosition = col.transform.localPosition;
                det.LastLocalRotation = col.transform.localRotation;
            }

            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            skinnedMeshAnimator.enabled = false;
            skinnedMeshAnimator.avatar = null;

            foreach (Collider col in RagdollParts)
            {
                col.isTrigger = false;
                col.attachedRigidbody.velocity = Vector3.zero;

                TriggerDetector det = col.GetComponent<TriggerDetector>();
                col.transform.localPosition = det.LastLocalPosition;
                col.transform.localRotation = det.LastLocalRotation;
            }
        }

        public List<TriggerDetector> GetAllTriggerDetectors()
        {
            if (TriggerDetectors.Count == 0)
            {
                TriggerDetector[] Triggers = GetComponentsInChildren<TriggerDetector>();
                foreach (TriggerDetector t in Triggers)
                {
                    TriggerDetectors.Add(t);
                }
            }
            return TriggerDetectors;
        }

        public void UpdateTargetSize()
        {
            if (!AnimProgress.IsUpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(PlayerBoxCollider.size - AnimProgress.TargetSize) > 0.1f)
            {
                PlayerBoxCollider.size = Vector3.Lerp(PlayerBoxCollider.size, AnimProgress.TargetSize, AnimProgress.SizeSpeed * Time.deltaTime);
                AnimProgress.IsUpdatingSpheres = true;
            }
        }

        public void UpdateTargetCenter()
        {
            if (!AnimProgress.IsUpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(PlayerBoxCollider.center - AnimProgress.TargetCenter) > 0.1f)
            {
                PlayerBoxCollider.center = Vector3.Lerp(PlayerBoxCollider.center, AnimProgress.TargetCenter, AnimProgress.CenterSpeed * Time.deltaTime);
                AnimProgress.IsUpdatingSpheres = true;
            }
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity -= Vector3.up * GravityMultiplier;
            }

            if (RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity -= Vector3.up * PullMultiplier;
            }
            AnimProgress.IsUpdatingSpheres = false;
            UpdateTargetSize();
            UpdateTargetCenter();

            if (AnimProgress.IsUpdatingSpheres)
            {
                RepositionSpheres("front");
                RepositionSpheres("bottom");
            }

        }
        private void SetUpSphereEdge()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = CreatePrefabSphereEdge(Vector3.zero);
                FrontSpheres.Add(obj);
            }

            RepositionSpheres("front");

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = CreatePrefabSphereEdge(Vector3.zero);
                BottomSpheres.Add(obj);
            }

            RepositionSpheres("bottom");

        }

        public void RepositionSpheres(string directionType)
        {
            float bottom = PlayerBoxCollider.bounds.center.y - PlayerBoxCollider.bounds.size.y / 2f;
            float top = PlayerBoxCollider.bounds.center.y + PlayerBoxCollider.bounds.size.y / 2f;
            float front = PlayerBoxCollider.bounds.center.z + PlayerBoxCollider.bounds.size.z / 2f;
            float back = PlayerBoxCollider.bounds.center.z - PlayerBoxCollider.bounds.size.z / 2f;

            switch (directionType)
            {
                case "front":
                    {
                        FrontSpheres[0].transform.localPosition = new Vector3(0f, bottom + 0.05f, front) - transform.position;
                        FrontSpheres[1].transform.localPosition = new Vector3(0f, top, front) - transform.position;

                        float interval = (top - bottom + 0.05f) / 9;

                        for (int i = 1; i < FrontSpheres.Count; i++)
                        {
                            FrontSpheres[i].transform.localPosition = new Vector3(0f, bottom + (interval * i), front) - transform.position;
                        }
                        break;
                    }
                case "bottom":
                    {
                        BottomSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - transform.position;
                        BottomSpheres[1].transform.localPosition = new Vector3(0, bottom, front) - transform.position;

                        float interval = (front - back) / 4;

                        for (int i = 1; i < BottomSpheres.Count; i++)
                        {
                            BottomSpheres[i].transform.localPosition = new Vector3(0f, bottom, back + (interval * i)) - transform.position;
                        }
                        break;
                    }
            }
        }

        private GameObject CreatePrefabSphereEdge(Vector3 position)
        {
            return Instantiate(Resources.Load("SpherePrefabEdge", typeof(GameObject)), position, Quaternion.identity, transform) as GameObject;
        }

        public void MoveAbleCharacter(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        public void changeMaterial()
        {
            if (null == mat)
            {
                Debug.LogError("No Material Specified");
            }

            Renderer[] arr = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arr)
            {
                if (this.gameObject != r.gameObject)
                {
                    r.material = mat;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoints = collision.contacts;
        }

        public void SetFaceForward(bool isFacing)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(SceneBuilder.CharacterSelectScene.ToString()))
            {
                return;
            }

            if (isFacing)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0);
            }
        }

        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

