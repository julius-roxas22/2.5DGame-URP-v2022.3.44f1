using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/GrounDetector")]
    public class GroundDetector : StateData
    {
        [Range(0.01f, 1)]
        public float CheckTime;
        public float Distance;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= CheckTime)
            {
                if (IsGrounded(characterControl))
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), false);
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control)
        {
            if (null != control.ContactPoints)
            {
                foreach (ContactPoint c in control.ContactPoints)
                {
                    float colliderBottom = (control.transform.position.y + control.PlayerBoxCollider.center.y) - (control.PlayerBoxCollider.size.y / 2f);
                    float yDiff = Mathf.Abs(colliderBottom - c.point.y);

                    if (yDiff < 0.01f)
                    {
                        if (Mathf.Abs(control.RIGID_BODY.velocity.y) < 0.001f)
                        {
                            return true;
                        }
                    }
                }
            }

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in control.BottomSpheres)
                {
                    RaycastHit hit;
                    Debug.DrawRay(o.transform.position, Vector3.down * Distance, Color.red);
                    if (Physics.Raycast(o.transform.position, Vector3.down, out hit, Distance))
                    {
                        if (!control.RagdollParts.Contains(hit.collider)
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject)
                            && !Ledge.IsCharacter(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}

