using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/SpawnObject")]
    public class SpawnObject : StateData
    {
        [Range(0f, 1f)]
        [SerializeField] private float SpawnTiming;
        [SerializeField] private string ParentObjName = string.Empty;
        [SerializeField] private PoolObjectType poolObjectType;
        [SerializeField] private bool StickToParent;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f)
            {
                SpawnObj(characterControl);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterControl.AnimProgress.PoolObjectList.Contains(poolObjectType) && stateInfo.normalizedTime >= SpawnTiming)
            {
                SpawnObj(characterControl);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.AnimProgress.PoolObjectList.Contains(poolObjectType))
            {
                characterControl.AnimProgress.PoolObjectList.Remove(poolObjectType);
            }
        }

        private void SpawnObj(CharacterControl characterControl)
        {
            if (characterControl.AnimProgress.PoolObjectList.Contains(poolObjectType))
            {
                return;
            }

            GameObject obj = PoolManager.Instance.InstantiatePrefab(poolObjectType);

            if (!string.IsNullOrEmpty(ParentObjName))
            {
                GameObject getParent = characterControl.GetChildObj(ParentObjName);
                obj.transform.parent = getParent.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!StickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);
            characterControl.AnimProgress.PoolObjectList.Add(poolObjectType);
        }
    }
}

