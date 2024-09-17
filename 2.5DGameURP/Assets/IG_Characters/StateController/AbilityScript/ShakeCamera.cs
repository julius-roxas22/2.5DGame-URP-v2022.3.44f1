using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0f, 1f)]
        [SerializeField] private float ShakeTiming;
        private bool IsShaking;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(.3f);
                IsShaking = true;
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsShaking && stateInfo.normalizedTime >= ShakeTiming)
            {
                CameraManager.Instance.ShakeCamera(.3f);
                IsShaking = true;
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            IsShaking = false;
        }
    }
}

