using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum CharacterColorType
    {
        NONE,
        BLUE,
        RED,
        GREEN
    }

    [CreateAssetMenu(fileName = "New CharacterSelect Data", menuName = "IndieGameDev/CharacterSelect/Object")]
    public class CharacterSelect : ScriptableObject
    {
        public CharacterColorType characterType;
    }
}

