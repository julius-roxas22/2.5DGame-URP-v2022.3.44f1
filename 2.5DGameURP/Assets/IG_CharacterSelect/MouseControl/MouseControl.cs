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
        private GameObject RingLight;
        private CharacterSelectCameraController CamController;

        private void Awake()
        {
            characterSelect.characterType = CharacterColorType.NONE;
            selectedLight = FindObjectOfType<SelectedLight>();
            hoverLight = FindObjectOfType<HoverLight>();
            RingLight = GameObject.Find("RingLight");
            RingLight.SetActive(false);
            CamController = FindObjectOfType<CharacterSelectCameraController>();
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
                    selectedLight.transform.position = hoverLight.transform.position;

                    CharacterControl characterControl = CharacterManager.Instance.GetCharacterControl(characterSelect.characterType);
                    selectedLight.transform.parent = characterControl.skinnedMeshAnimator.transform;
                    selectedLight.spotLight.enabled = true;

                    RingLight.SetActive(true);
                    RingLight.transform.parent = characterControl.skinnedMeshAnimator.transform;
                    RingLight.transform.localPosition = Vector3.zero;
                    RingLight.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    characterSelect.characterType = CharacterColorType.NONE;
                    selectedLight.spotLight.enabled = false;
                    RingLight.SetActive(false);
                }

                foreach (CharacterControl characterControl in CharacterManager.Instance.characters)
                {
                    if (characterControl.characterColorType == characterColorType)
                    {
                        characterControl.skinnedMeshAnimator.SetBool(TransitionParameters.ClickAnimation.ToString(), true);
                    }
                    else
                    {
                        characterControl.skinnedMeshAnimator.SetBool(TransitionParameters.ClickAnimation.ToString(), false);
                    }
                }
                CamController.SwitchCameraSelected(characterSelect.characterType);
            }
        }
    }
}