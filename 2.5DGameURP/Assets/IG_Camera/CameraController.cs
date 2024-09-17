using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum CameraTrigger
    {
        Default,
        Shake
    }

    public class CameraController : MonoBehaviour
    {
        private Animator animator;

        public Animator GetCamAnimatorController
        {
            get
            {
                if (null == animator)
                {
                    animator = GetComponent<Animator>();
                }
                return animator;
            }
        }

        public void TriggerCamera(CameraTrigger trigger)
        {
            GetCamAnimatorController.SetTrigger(trigger.ToString());
        }
    }
}

