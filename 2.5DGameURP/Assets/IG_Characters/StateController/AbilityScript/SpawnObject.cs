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
            GameObject obj = PoolManager.Instance.InstantiatePrefab(PoolObjectType.HAMMER);
            WeaponSpawn weaponSpawn = characterControl.GetComponentInChildren<WeaponSpawn>();
            obj.transform.parent = weaponSpawn.transform;
            obj.transform.position = weaponSpawn.transform.position;
            obj.transform.rotation = weaponSpawn.transform.rotation;
            obj.SetActive(true);
        }
    }
}

