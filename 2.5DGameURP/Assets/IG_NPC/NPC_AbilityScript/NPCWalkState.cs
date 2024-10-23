using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/NPCWalkState")]
    public class NPCWalkState : StateData
    {
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            Vector3 dis = characterControl.NPCAnimProgress.agent.OffMeshStartPosition - characterControl.transform.position;

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
            Vector3 dist = characterControl.NPCAnimProgress.agent.OffMeshStartPosition - characterControl.transform.position;

            if (dist.sqrMagnitude < 0.8f)
            {
                characterControl.MoveRight = false;
                characterControl.MoveLeft = false;

                if (characterControl.NPCAnimProgress.agent.EndSphere.transform.position.y > characterControl.NPCAnimProgress.agent.StartSphere.transform.position.y)
                {
                    animator.SetBool(NPCTransitionParameters.NPCJump.ToString(), true);
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(NPCTransitionParameters.NPCJump.ToString(), false);
        }
    }
}

