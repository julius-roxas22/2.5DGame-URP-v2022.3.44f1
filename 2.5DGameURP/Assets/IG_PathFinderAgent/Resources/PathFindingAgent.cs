using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGameDev
{
    public class PathFindingAgent : MonoBehaviour
    {
        [SerializeField] private bool TargetPlayableCharacter;

        private List<Coroutine> MoveRoutines = new List<Coroutine>();
        private GameObject target;
        private NavMeshAgent agent;

        public GameObject StartSphere;
        public GameObject EndSphere;

        public bool NPCMove;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        IEnumerator IEOnTargetStep()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {

                    StartSphere.transform.position = agent.currentOffMeshLinkData.startPos;
                    EndSphere.transform.position = agent.currentOffMeshLinkData.endPos;

                    agent.CompleteOffMeshLink();

                    agent.isStopped = true;
                    NPCMove = true;
                    yield break;
                }

                Vector3 sqrtDistance = transform.position - agent.destination;

                if (sqrtDistance.sqrMagnitude < 0.5f)
                {
                    StartSphere.transform.position = agent.destination;
                    EndSphere.transform.position = agent.destination;
                    agent.isStopped = true;
                    NPCMove = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        public void GoToDistination()
        {
            agent.enabled = true;

            StartSphere.transform.parent = null;
            EndSphere.transform.parent = null;

            NPCMove = false;

            agent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }

            agent.SetDestination(target.transform.position);

            if (MoveRoutines.Count != 0)
            {
                StopCoroutine(MoveRoutines[0]);
                MoveRoutines.RemoveAt(0);
            }
            MoveRoutines.Add(StartCoroutine(IEOnTargetStep()));
        }
    }
}