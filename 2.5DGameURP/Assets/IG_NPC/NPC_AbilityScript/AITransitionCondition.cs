using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    public enum AITransitionType
    {
        RUN_TO_WALK,
        WALK_TO_RUN,
    }

    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/AITransitionCondition")]
    public class AITransitionCondition : StateData
    {
        public AITransitionType InitialTransitionType;
        public AI_TYPE NextAi;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionToNextAI(characterControl))
            {
                characterControl.NPCController.TriggerAI(NextAi);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool TransitionToNextAI(CharacterControl characterControl)
        {
            if (InitialTransitionType == AITransitionType.RUN_TO_WALK)
            {
                Vector3 dist = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;
                if (Vector3.SqrMagnitude(dist) < 2f)
                {
                    return true;
                }
            }
            else if (InitialTransitionType == AITransitionType.WALK_TO_RUN)
            {
                Vector3 dist = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;
                if (Vector3.SqrMagnitude(dist) > 2f)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

