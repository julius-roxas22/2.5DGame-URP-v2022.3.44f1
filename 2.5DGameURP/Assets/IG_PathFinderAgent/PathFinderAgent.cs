using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    public class PathFinderAgent : MonoBehaviour
    {
        [SerializeField] private bool TargetPlayableCharacter;

        private Coroutine MoveRoutine;
        private GameObject target;
        private NavMeshAgent agent;

        public Vector3 OffMeshStartPosition, OffMeshEndPosition, CurrentPosition;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            CurrentPosition = transform.position;
        }

        IEnumerator IEOnTargetStep()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    OffMeshStartPosition = CurrentPosition;
                    agent.CompleteOffMeshLink();
                    yield return new WaitForEndOfFrame();
                    OffMeshEndPosition = CurrentPosition;
                    agent.isStopped = true;
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        public void TowardsToTarget()
        {
            agent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }

            agent.SetDestination(target.transform.position);

            if (null != MoveRoutine)
            {
                StopCoroutine(MoveRoutine);
            }

            MoveRoutine = StartCoroutine(IEOnTargetStep());
        }
    }
}