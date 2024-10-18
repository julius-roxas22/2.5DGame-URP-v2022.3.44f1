using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 offset;
        public Vector3 endPosition;

        public static bool IsLedge(GameObject obj)
        {
            if (null == obj.GetComponent<Ledge>())
            {
                return false;
            }
            return true;
        }

        public static bool IsLedgeChecker(GameObject obj)
        {
            if (null == obj.GetComponent<LedgeChecker>())
            {
                return false;
            }
            return true;
        }
    }
}


