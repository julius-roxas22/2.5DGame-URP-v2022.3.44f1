using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class SelectedLight : MonoBehaviour
    {
        public Light spotLight;

        void Start()
        {
            spotLight = GetComponent<Light>();
            spotLight.enabled = false;
        }
    }
}


