using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/ToggerGravity")]
    public class ToggerGravity : StateData
    {
        public bool OnEnabled;
        public bool OnStart;
        public bool OnEnd;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                OnEnableGravity(characterControl);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                OnEnableGravity(characterControl);
            }
        }

        private void OnEnableGravity(CharacterControl characterControl)
        {
            characterControl.RIGID_BODY.velocity = Vector3.zero;
            characterControl.RIGID_BODY.useGravity = OnEnabled;
        }
    }
}

