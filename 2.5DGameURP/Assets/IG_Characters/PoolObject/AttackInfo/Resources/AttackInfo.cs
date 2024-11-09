using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public Attack AttackAbility;
        //public List<string> CollidingNames = new List<string>();
        public List<AttackType> AttackTypes = new List<AttackType>();
        public bool MustCollide;
        public DeathType deathType;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegistered;
        public bool isFinished;

        public void ResetInfo(Attack attack, CharacterControl attacker)
        {
            isRegistered = false;
            isFinished = false;
            AttackAbility = attack;
            Attacker = attacker;
        }

        public void RegisterAttack(Attack attack)
        {
            isRegistered = true;

            AttackAbility = attack;
            deathType = attack.deathType;
            AttackTypes = attack.AttackTypes;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.Maxhits;
            CurrentHits = 0;
        }

        private void OnDisable()
        {
            isFinished = true;
        }

    }
}
