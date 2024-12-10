using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{

    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl control;
        private GeneralBodyParts DamagePart;
        public int DamageTaken;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (null == info)
                {
                    continue;
                }

                if (!info.isRegistered)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                if (info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }

                if (info.Attacker == control)
                {
                    continue;
                }

                if (info.MustFaceAttacker)
                {
                    Vector3 vec = control.transform.position - info.Attacker.transform.position;
                    if (vec.z * info.Attacker.transform.forward.z < 0f)
                    {
                        continue;
                    }
                }

                if (info.MustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
                else
                {
                    float dist = Vector3.SqrMagnitude(control.transform.position - info.Attacker.transform.position);
                    if (dist <= info.LethalRange)
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach (TriggerDetector trigger in control.GetAllTriggerDetectors())
            {
                foreach (Collider col in trigger.CollidingParts)
                {
                    foreach (AttackType p in info.AttackTypes)
                    {
                        if (p == AttackType.RIGHT_HAND)
                        {
                            if (col.gameObject == info.Attacker.RightHandAttack)
                            {
                                DamagePart = trigger.BodyPart;
                                return true;

                            }
                        }
                        else if (p == AttackType.LEFT_HAND)
                        {
                            if (col.gameObject == info.Attacker.LeftHandAttack)
                            {
                                DamagePart = trigger.BodyPart;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            if (DamageTaken > 0)
            {
                return;
            }

            if (info.MustCollide)
            {
                CameraManager.Instance.ShakeCamera(.3f);
            }

            control.skinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetDeathAnimation(DamagePart, info);
            info.CurrentHits++;
            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
            control.navMeshObstacle.carving = false;

            DamageTaken++;
        }
    }

}
