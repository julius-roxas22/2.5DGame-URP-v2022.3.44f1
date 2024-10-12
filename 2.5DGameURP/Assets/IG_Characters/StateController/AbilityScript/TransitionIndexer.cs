using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        JUMP,
        ATTACK,
        LEDGE_GRAB
    }

    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {
        [SerializeField] private int index;
        [SerializeField] private List<TransitionConditionType> ConditionTypes = new List<TransitionConditionType>();
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (MakeTransition(characterControl))
            {
                animator.SetInteger(TransitionParameters.TransitionIndex.ToString(), index);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (MakeTransition(characterControl))
            {
                animator.SetInteger(TransitionParameters.TransitionIndex.ToString(), index);
            }
            else
            {
                animator.SetInteger(TransitionParameters.TransitionIndex.ToString(), 0);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(TransitionParameters.TransitionIndex.ToString(), 0);
        }

        private bool MakeTransition(CharacterControl characterControl)
        {
            foreach (TransitionConditionType type in ConditionTypes)
            {
                switch (type)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!characterControl.MoveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!characterControl.MoveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!characterControl.MoveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!characterControl.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!characterControl.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!characterControl.Attack)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEDGE_GRAB:
                        {
                            if (!characterControl.ledgeChecker.IsGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            return true;
        }
    }
}

