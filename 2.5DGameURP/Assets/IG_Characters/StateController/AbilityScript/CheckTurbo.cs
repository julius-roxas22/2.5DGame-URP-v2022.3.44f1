using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        [SerializeField] private bool MustRequireMovement;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.Turbo)
            {
                if (MustRequireMovement)
                {
                    if (characterControl.MoveLeft || characterControl.MoveRight)
                    {
                        animator.SetBool(TransitionParameters.Turbo.ToString(), true);
                    }
                    else
                    {
                        animator.SetBool(TransitionParameters.Turbo.ToString(), false);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameters.Turbo.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(TransitionParameters.Turbo.ToString(), false);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

