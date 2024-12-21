using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        [SerializeField] private float JumpTiming;
        [SerializeField] private float jumpForce;

        [Header("Extra Gravity")]
        [SerializeField] private AnimationCurve PullMultiplierGraph;
        public bool CancelPull;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.AnimProgress.Jumped = false;
            if (JumpTiming == 0f)
            {
                characterControl.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                characterControl.AnimProgress.Jumped = true;
            }
            animator.SetBool(TransitionParameters.Grounded.ToString(), false);
            characterControl.AnimProgress.CancelPull = CancelPull;
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.PullMultiplier = PullMultiplierGraph.Evaluate(stateInfo.normalizedTime);
            if (!characterControl.AnimProgress.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterControl.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                characterControl.AnimProgress.Jumped = true;
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.PullMultiplier = 0f;
            //characterControl.AnimProgress.Jumped = false;
        }
    }
}

