using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CharacterSelectCameraState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterColorType[] arr = System.Enum.GetValues(typeof(CharacterColorType)) as CharacterColorType[];
            foreach (CharacterColorType c in arr)
            {
                animator.SetBool(c.ToString(), false);
            }
        }
    }
}