using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class PlayGame : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        void Update()
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (characterSelect.characterType != CharacterColorType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneBuilder.MainScene.ToString());
                }
                else
                {
                    Debug.LogError("Please Select Character first");
                }
            }
        }
    }

}