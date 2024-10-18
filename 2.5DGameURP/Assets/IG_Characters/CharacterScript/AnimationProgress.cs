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

        private float PressTime;

        private CharacterControl characterControl;

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
    }
}

