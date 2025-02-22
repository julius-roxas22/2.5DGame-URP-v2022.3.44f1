using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Idle")]
    public class Idle : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Jump.ToString(), false);
            animator.SetBool(TransitionParameters.Grounded.ToString(), true);
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
            animator.SetBool(TransitionParameters.Move.ToString(), false);
            characterControl.AnimProgress.disAllowEarlyTurn = false;
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.MoveRight && characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (characterControl.AnimProgress.AttackTriggered /*characterControl.Attack*/)
            {
                animator.SetBool(TransitionParameters.Attack.ToString(), true);
            }

            if (characterControl.Jump)
            {
                if (!characterControl.AnimProgress.Jumped)
                {
                    animator.SetBool(TransitionParameters.Jump.ToString(), true);
                }
            }
            else
            {
                characterControl.AnimProgress.Jumped = false;
            }

            if (characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }

            if (characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
        }
    }
}

