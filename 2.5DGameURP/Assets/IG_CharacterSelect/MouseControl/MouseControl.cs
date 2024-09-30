using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class MouseControl : MonoBehaviour
    {
        public CharacterColorType characterColorType;
        public CharacterSelect characterSelect;
        private Ray ray;
        private RaycastHit hit;
        private SelectedLight selectedLight;
        private HoverLight hoverLight;

        private void Awake()
        {
            characterSelect.characterType = CharacterColorType.NONE;
            selectedLight = FindObjectOfType<SelectedLight>();
            hoverLight = FindObjectOfType<HoverLight>();
        }

        private void Update()
        {
            ray = CameraManager.Instance.MAIN_CAMERA.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl characterControl = hit.transform.gameObject.GetComponent<CharacterControl>();
                if (null == characterControl)
                {
                    characterColorType = CharacterColorType.NONE;
                }
                else
                {
                    characterColorType = characterControl.characterColorType;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (characterColorType != CharacterColorType.NONE)
                {
                    characterSelect.characterType = characterColorType;
                    selectedLight.spotLight.enabled = true;
                    selectedLight.transform.position = hoverLight.transform.position;
                }
                else
                {
                    characterSelect.characterType = CharacterColorType.NONE;
                    selectedLight.spotLight.enabled = false;
                }
            }
        }
    }
}