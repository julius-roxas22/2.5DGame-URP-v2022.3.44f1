using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/CheckRunningTurn")]
    public class CheckRunningTurn : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.IsFacingForward() && characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Turn.ToString(), true);
            }

            if (!characterControl.IsFacingForward() && characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameters.Turn.ToString(), true);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Turn.ToString(), false);
        }
    }
}

