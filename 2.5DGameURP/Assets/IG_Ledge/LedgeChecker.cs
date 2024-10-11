using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class LedgeChecker : MonoBehaviour
    {
        private Ledge ledge = null;
        public bool IsGrabbingLedge;
        private void OnTriggerEnter(Collider other)
        {
            ledge = other.gameObject.GetComponent<Ledge>();
            if (null != ledge)
            {
                IsGrabbingLedge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IsGrabbingLedge = false;
        }
    }
}