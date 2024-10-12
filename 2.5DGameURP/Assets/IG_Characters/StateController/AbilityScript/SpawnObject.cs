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

        private bool IsSpawned;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f)
            {
                SpawnObj(characterControl);
                IsSpawned = true;
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsSpawned && stateInfo.normalizedTime >= SpawnTiming)
            {
                SpawnObj(characterControl);
                IsSpawned = true;
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            IsSpawned = false;
        }

        private void SpawnObj(CharacterControl characterControl)
        {
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
        }
    }
}

