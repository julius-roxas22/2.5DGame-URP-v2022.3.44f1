using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField]
        private CharacterSelect characterSelect;

        private string playerName;
        void Start()
        {
            switch (characterSelect.characterType)
            {
                case CharacterColorType.BLUE:
                    {
                        playerName = "yBot - Blue";
                        break;
                    }
                case CharacterColorType.RED:
                    {
                        playerName = "yBot - Red";
                        break;
                    }
                case CharacterColorType.GREEN:
                    {
                        playerName = "yBot - Green";
                        break;
                    }
            }

            GameObject objPlayer = Instantiate(Resources.Load(playerName, typeof(GameObject))) as GameObject;
            objPlayer.GetComponent<ManualInput>().enabled = true;
            objPlayer.transform.position = transform.position;
            GetComponent<MeshRenderer>().enabled = false;

            CharacterControl characterControl = objPlayer.GetComponent<CharacterControl>();
            Transform target = characterControl.FindTargetCameraLimb("mixamorig:Spine1").transform;

            Cinemachine.CinemachineVirtualCamera[] cameras = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            foreach (Cinemachine.CinemachineVirtualCamera cam in cameras)
            {
                cam.LookAt = target;
                cam.Follow = target;
            }
        }

        void Update()
        {

        }
    }
}