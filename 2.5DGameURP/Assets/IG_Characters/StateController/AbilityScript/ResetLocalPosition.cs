using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/ResetLocalPosition")]
    public class ResetLocalPosition : StateData
    {
        [SerializeField] private bool OnStart;
        [SerializeField] private bool OnEnd;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                characterControl.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterControl.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                characterControl.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterControl.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}

