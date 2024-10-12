using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl newCharacterControl = CharacterManager.Instance.GetCharacterControl(animator);

            Vector3 endPosition = newCharacterControl.ledgeChecker.GrabbLedge.transform.position + newCharacterControl.ledgeChecker.GrabbLedge.endPosition;

            newCharacterControl.skinnedMeshAnimator.transform.parent = newCharacterControl.transform;
            newCharacterControl.transform.position = endPosition;
            newCharacterControl.skinnedMeshAnimator.transform.position = endPosition;
        }
    }
}

