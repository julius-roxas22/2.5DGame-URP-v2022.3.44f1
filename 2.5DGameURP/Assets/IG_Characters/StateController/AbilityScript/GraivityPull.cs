using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/GravityPull")]
    public class GravityPull : StateData
    {
        [SerializeField] private AnimationCurve GraivityPullMutiplier;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.GravityMultiplier = GraivityPullMutiplier.Evaluate(stateInfo.normalizedTime);
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.GravityMultiplier = 0f;
        }
    }
}

