using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/CheckTurboAndMovement")]
    public class CheckTurboAndMovement : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if ((characterControl.MoveLeft || characterControl.MoveRight) && characterControl.Turbo)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
                animator.SetBool(TransitionParameters.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                animator.SetBool(TransitionParameters.Turbo.ToString(), false);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

