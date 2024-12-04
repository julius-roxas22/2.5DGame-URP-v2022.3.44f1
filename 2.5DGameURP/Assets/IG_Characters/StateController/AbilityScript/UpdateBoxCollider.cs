using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        public Vector3 TargetCenter;
        public float CenterSpeed;
        [Space(10)]
        public Vector3 TargetSize;
        public float SizeSpeed;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.AnimProgress.IsUpdatingBoxCollider = true;

            characterControl.AnimProgress.TargetSize = TargetSize;
            characterControl.AnimProgress.SizeSpeed = SizeSpeed;
            characterControl.AnimProgress.TargetCenter = TargetCenter;
            characterControl.AnimProgress.CenterSpeed = CenterSpeed;
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.AnimProgress.IsUpdatingBoxCollider = false;
        }
    }
}

