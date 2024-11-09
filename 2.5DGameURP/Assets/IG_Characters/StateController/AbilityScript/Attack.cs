using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum AttackType
    {
        RIGHT_HAND,
        LEFT_HAND
    }

    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Attack")]
    public class Attack : StateData
    {
        public float StartAttackTime;
        public float EndTimeAttack;
        //public List<string> ColliderNames = new List<string>();
        public List<AttackType> AttackTypes = new List<AttackType>();
        public DeathType deathType;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int Maxhits;
        private List<AttackInfo> FinishedAttack = new List<AttackInfo>();
        public bool debug;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);

            GameObject obj = PoolManager.Instance.InstantiatePrefab(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);

            info.ResetInfo(this, characterControl);

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisteredAttack(characterControl, stateInfo);
            DeRegistered(characterControl, stateInfo);
            CheckCombo(characterControl, animator, stateInfo);
        }

        public void RegisteredAttack(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndTimeAttack > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.AttackAbility && !info.isRegistered)
                    {
                        info.RegisterAttack(this);
                        if (debug)
                        {
                            Debug.Log(info.AttackAbility.name + " registerd " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        public void DeRegistered(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndTimeAttack)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.AttackAbility && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();
                        if (debug)
                        {
                            Debug.Log(info.AttackAbility.name + " de-registerd " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        public void CheckCombo(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= StartAttackTime + ((EndTimeAttack - StartAttackTime) / 3))
            {
                if (stateInfo.normalizedTime < EndTimeAttack + ((EndTimeAttack - StartAttackTime) / 2f))
                {
                    if (characterControl.AnimProgress.AttackTriggered /*characterControl.Attack*/)
                    {
                        animator.SetBool(TransitionParameters.Attack.ToString(), true);
                    }
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
            ClearAttack();
        }

        public void ClearAttack()
        {
            FinishedAttack.Clear();

            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (null == info || this == info.AttackAbility /*info.isFinished*/)
                {
                    FinishedAttack.Add(info);
                }
            }

            foreach (AttackInfo info in FinishedAttack)
            {
                if (AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }
    }
}

