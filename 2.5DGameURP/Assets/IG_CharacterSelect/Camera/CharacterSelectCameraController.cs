using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CharacterSelectCameraController : MonoBehaviour
    {
        private Animator skinnedMesh;

        public Animator CharacterSelectCamController
        {
            get
            {
                if (null == skinnedMesh)
                {
                    skinnedMesh = GetComponent<Animator>();
                }
                return skinnedMesh;
            }
        }

        public void SwitchCameraSelected(CharacterColorType colorType)
        {
            CharacterSelectCamController.SetBool(colorType.ToString(), true);
        }
    }
}