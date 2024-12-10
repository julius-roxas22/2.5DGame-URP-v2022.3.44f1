using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/NPCJumpState")]
    public class NPCJump : StateData
    {
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.Jump = true;
            characterControl.MoveUp = true;

            //if (characterControl.NPCAnimProgress.agent.StartSphere.transform.position.z < characterControl.NPCAnimProgress.agent.EndSphere.transform.position.z)
            //{
            //    characterControl.SetFaceForward(true);
            //}
            //else
            //{
            //    characterControl.SetFaceForward(false);
            //}

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
            float topDist = characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y - characterControl.FrontSpheres[1].transform.position.y;

            float bottomDist = characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y - characterControl.FrontSpheres[0].transform.position.y;

            if (topDist < 0.5f && bottomDist > 1f)
            {
                if (characterControl.IsFacingForward())
                {
                    characterControl.MoveRight = true;
                    characterControl.MoveLeft = false;
                }
                else
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = true;
                }
            }

            if (bottomDist < 0.5f)
            {
                characterControl.MoveRight = false;
                characterControl.MoveLeft = false;
                characterControl.MoveUp = false;
                characterControl.Jump = false;

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

