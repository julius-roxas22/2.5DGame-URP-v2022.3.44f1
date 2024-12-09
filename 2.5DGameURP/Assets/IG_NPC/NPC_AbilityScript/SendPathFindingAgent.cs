using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/SendPathFindingAgent")]
    public class SendPathFindingAgent : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (null == characterControl.NPCAnimProgress.agent)
            {
                GameObject pathAgentObj = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject))) as GameObject;
                characterControl.NPCAnimProgress.agent = pathAgentObj.GetComponent<PathFindingAgent>();
            }

            PathFindingAgent agent = characterControl.NPCAnimProgress.agent;
            agent.GetComponent<NavMeshAgent>().enabled = false;
            agent.transform.position = characterControl.transform.position;
            characterControl.navMeshObstacle.carving = false;
            agent.owner = characterControl;
            agent.GoToDistination();
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            PathFindingAgent agent = characterControl.NPCAnimProgress.agent;
            if (agent.NPCStartWalk)
            {
                animator.SetBool(NPCTransitionParameters.NPCWalk.ToString(), true);
                animator.SetBool(NPCTransitionParameters.NPCRun.ToString(), true);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(NPCTransitionParameters.NPCWalk.ToString(), false);
            animator.SetBool(NPCTransitionParameters.NPCRun.ToString(), false);
        }
    }
}

