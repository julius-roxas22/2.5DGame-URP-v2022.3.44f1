using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class HoverLight : MonoBehaviour
    {
        public Vector3 offset;
        private CharacterControl characterControlPos;
        private MouseControl mouseControlPos;
        private Light spotLight;
        private Vector3 targetPos;


        void Start()
        {
            mouseControlPos = FindObjectOfType<MouseControl>();
            spotLight = GetComponent<Light>();
            spotLight.enabled = false;
        }

        void Update()
        {
            if (mouseControlPos.characterColorType != CharacterColorType.NONE)
            {
                spotLight.enabled = true;
                LightUpHoverCharacter();
            }
            else
            {
                characterControlPos = null;
                spotLight.enabled = false;
            }
        }

        private void LightUpHoverCharacter()
        {
            if (null == characterControlPos)
            {
                characterControlPos = CharacterManager.Instance.GetCharacterControl(mouseControlPos.characterColorType);
                //transform.position = characterControlPos.transform.position + characterControlPos.transform.TransformDirection(offset);
                transform.position = characterControlPos.transform.position + offset;
            }
        }
    }
}


