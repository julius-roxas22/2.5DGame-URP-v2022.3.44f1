using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/NPCWalkState")]
    public class NPCWalk : StateData
    {
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

            Vector3 dis = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;

            if (dis.z > 0f)
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

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            Vector3 dist = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;

            if (characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y > characterControl.NPCAnimProgress.agent.StartSphere.transform.position.y)
            {
                if (dist.sqrMagnitude < 0.01f)
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = false;
                    animator.SetBool(NPCTransitionParameters.NPCJump.ToString(), true);
                }
            }

            if (characterControl.NPCAnimProgress.agent.StartSphere.transform.position.y > characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y)
            {
                animator.SetBool(NPCTransitionParameters.NPCFall.ToString(), true);
            }

            if (characterControl.NPCAnimProgress.agent.StartSphere.transform.position.y == characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y)
            {
                if (dist.sqrMagnitude < 1f)
                {
                    characterControl.MoveRight = false;
                    characterControl.MoveLeft = false;

                    Vector3 playerDist = characterControl.transform.position - CharacterManager.Instance.GetPlayableCharacter().transform.position;

                    if (playerDist.sqrMagnitude > 1.4f)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                    /* else
                    {
                         if (CharacterManager.Instance.GetPlayableCharacter().damageDetector.DamageTaken == 0)
                         {
                             if (characterControl.IsFacingForward())
                             {
                                 characterControl.MoveRight = true;
                                 characterControl.MoveLeft = false;
                                 characterControl.Attack = true;
                             }
                             else
                             {
                                 characterControl.MoveRight = false;
                                 characterControl.MoveLeft = true;
                                 characterControl.Attack = true;
                             }
                         }
                     }*/
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(NPCTransitionParameters.NPCJump.ToString(), false);
            animator.SetBool(NPCTransitionParameters.NPCFall.ToString(), false);
        }
    }
}

