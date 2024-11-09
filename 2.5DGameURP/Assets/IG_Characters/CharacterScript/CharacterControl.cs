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
        public GameObject SphereEdgePrefab;
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

            bool SwitchBack = false;

            if (!IsFacingForward())
            {
                SwitchBack = true;
            }

            SetFaceForward(true);

            SetUpSphereEdge();

            if (SwitchBack)
            {
                SetFaceForward(false);
            }

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
            RIGID_BODY.velocity = Vector3.zero;
            RIGID_BODY.useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            skinnedMeshAnimator.avatar = null;

            foreach (Collider col in RagdollParts)
            {
                col.attachedRigidbody.velocity = Vector3.zero;
                col.isTrigger = false;
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
        }
        private void SetUpSphereEdge()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFrontVer = CreatePrefabSphereEdge(new Vector3(0f, bottom + 0.05f, front));
            GameObject bottomFrontHor = CreatePrefabSphereEdge(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreatePrefabSphereEdge(new Vector3(0f, bottom, back));
            GameObject topFront = CreatePrefabSphereEdge(new Vector3(0f, top, front));

            BottomSpheres.Add(bottomFrontHor);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(bottomFrontVer);
            FrontSpheres.Add(topFront);

            float horSphereSection = (bottomBack.transform.position - bottomFrontHor.transform.position).magnitude / 5f;
            CreatePrefabSphereEdge(bottomBack, transform.forward, horSphereSection, 4, BottomSpheres);

            float verSphereSection = (bottomFrontVer.transform.position - topFront.transform.position).magnitude / 10f;
            CreatePrefabSphereEdge(bottomFrontVer, transform.up, verSphereSection, 9, FrontSpheres);
        }

        private void CreatePrefabSphereEdge(GameObject start, Vector3 direction, float section, int iteration, List<GameObject> spheres)
        {
            for (int i = 0; i < iteration; i++)
            {
                Vector3 position = start.transform.position + (direction * section * (i + 1));
                GameObject obj = CreatePrefabSphereEdge(position);
                spheres.Add(obj);
            }
        }

        private GameObject CreatePrefabSphereEdge(Vector3 position)
        {
            return Instantiate(SphereEdgePrefab, position, Quaternion.identity, transform);
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

