using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/AI_Ability/NPCRunState")]
    public class NPCRun : StateData
    {
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

            Vector3 dis = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;

            if (dis.z > 0f)
            {
                characterControl.SetFaceForward(true);
                characterControl.MoveRight = true;
                characterControl.MoveLeft = false;
            }
            else
            {
                characterControl.SetFaceForward(false);
                characterControl.MoveRight = false;
                characterControl.MoveLeft = true;
            }

            characterControl.Turbo = true;
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            Vector3 dist = characterControl.NPCAnimProgress.agent.StartSphere.transform.position - characterControl.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                characterControl.MoveRight = false;
                characterControl.MoveLeft = false;
                characterControl.Turbo = false;
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

