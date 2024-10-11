using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            GameObject skinnedMesh = characterControl.skinnedMeshAnimator.gameObject;
            Ledge GrabbedLedge = characterControl.ledgeChecker.GrabbLedge;

            skinnedMesh.transform.parent = GrabbedLedge.transform;
            skinnedMesh.transform.localPosition = GrabbedLedge.offset;
            //characterControl.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

