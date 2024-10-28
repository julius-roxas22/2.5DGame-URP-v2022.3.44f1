using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/NPCFall")]
    public class NPCFall : StateData
    {
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.transform.position.z < characterControl.NPCAnimProgress.agent.EndSphere.transform.position.z)
            {
                characterControl.SetFaceForward(true);
            }
            else
            {
                characterControl.SetFaceForward(false);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.IsFacingForward())
            {
                if (characterControl.transform.position.z < characterControl.NPCAnimProgress.agent.EndSphere.transform.position.z)
                {
                    characterControl.MoveRight = true;
                    characterControl.MoveLeft = false;
                }
                else
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
            else
            {
                if (characterControl.transform.position.z > characterControl.NPCAnimProgress.agent.EndSphere.transform.position.z)
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = true;
                }
                else
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

