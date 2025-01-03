using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool Jumped;
        public bool CameraShaken;
        public List<PoolObjectType> PoolObjectList = new List<PoolObjectType>();
        public bool AttackTriggered;
        public float MaxPressTime;
        public bool RagdollTriggered;

        [Header("GroundMovement")]
        public bool disAllowEarlyTurn;
        public bool LockDirectionNextState;

        [Header("Air Control")]
        public float AirMomentum;
        public bool FrameUpdated;
        public bool CancelPull;

        private float PressTime;
        private CharacterControl characterControl;

        [Header("Update Box Collider")]
        public bool IsUpdatingBoxCollider;
        public bool IsUpdatingSpheres;
        public Vector3 TargetSize;
        public float SizeSpeed;
        public Vector3 TargetCenter;
        public float CenterSpeed;

        private void Awake()
        {
            characterControl = GetComponent<CharacterControl>();
            PressTime = 0;
        }

        private void Update()
        {
            if (characterControl.Attack)
            {
                PressTime += Time.deltaTime;
            }
            else
            {
                PressTime = 0;
            }

            if (PressTime == 0)
            {
                AttackTriggered = false;
            }
            else if (PressTime > MaxPressTime)
            {
                AttackTriggered = false;
            }
            else
            {
                AttackTriggered = true;
            }
        }

        private void LateUpdate()
        {
            FrameUpdated = false;
        }
    }
}

