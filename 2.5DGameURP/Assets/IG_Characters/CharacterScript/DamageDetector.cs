using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{

    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl control;
        private GeneralBodyParts DamagePart;

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

                if (info.MustCollide)
                {
                    if (IsCollided(info))
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
                    foreach (string colName in info.CollidingNames)
                    {
                        if (colName.Equals(col.gameObject.name))
                        {
                            //if (info.Attacker != control)
                            //{
                            //}
                            DamagePart = trigger.BodyPart;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            info.CurrentHits++;
            control.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
            control.skinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetDeathAnimation(DamagePart, info); //info.AttackAbility.GetDeathAnimatorController();
            CameraManager.Instance.ShakeCamera(.3f);
        }
    }

}
