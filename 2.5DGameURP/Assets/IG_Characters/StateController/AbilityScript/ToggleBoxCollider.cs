using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {
        [SerializeField] private bool OnEnabled;
        [SerializeField] private bool OnStart;
        [SerializeField] private bool OnEnd;
        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                characterControl.GetComponent<BoxCollider>().enabled = OnEnabled;
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                characterControl.GetComponent<BoxCollider>().enabled = OnEnabled;
            }
        }
    }
}

