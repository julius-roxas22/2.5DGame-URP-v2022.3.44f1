using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class LedgeChecker : MonoBehaviour
    {
        private Ledge CheckLedge = null;
        public Ledge GrabbLedge;
        public bool IsGrabbingLedge;
        private void OnTriggerEnter(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (null != CheckLedge)
            {
                GrabbLedge = CheckLedge;
                IsGrabbingLedge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (null != CheckLedge)
            {
                IsGrabbingLedge = false;
            }
        }
    }
}