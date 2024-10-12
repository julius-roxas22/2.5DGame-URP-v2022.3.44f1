using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum DeathType
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
    }

    [CreateAssetMenu(fileName = "New Death Data", menuName = "IndieGameDev/DeathAnimationData/DefaultDeathData")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyParts> DamageBodyParts = new List<GeneralBodyParts>();
        public DeathType deathType;
        public RuntimeAnimatorController AnimatorController;
        public bool IsFacing;
    }
}


