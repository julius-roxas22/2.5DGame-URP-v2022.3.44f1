using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> characters = new List<CharacterControl>();

        public CharacterControl GetCharacterControl(CharacterColorType type)
        {
            foreach (CharacterControl character in characters)
            {
                if (type.Equals(character.characterColorType)) return character;
            }
            return null;
        }

        public CharacterControl GetCharacterControl(Animator animator)
        {
            foreach (CharacterControl character in characters)
            {
                if (animator.Equals(character.skinnedMeshAnimator)) return character;
            }
            return null;
        }
    }
}
