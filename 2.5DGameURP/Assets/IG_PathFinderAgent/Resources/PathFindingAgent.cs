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

        public Vector3 OffMeshStartPosition;
        public Vector3 OffMeshEndPosition;
        public GameObject StartSphere;
        public GameObject EndSphere;

        //[SerializeField] private Vector3 CurrentPosition;

        public bool NPCMove;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //CurrentPosition = transform.position;
        }

        IEnumerator IEOnTargetStep()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    OffMeshStartPosition = /*CurrentPosition*/ transform.position;
                    StartSphere.transform.position = /*CurrentPosition*/ transform.position;
                    agent.CompleteOffMeshLink();

                    yield return new WaitForEndOfFrame();

                    OffMeshEndPosition = /*CurrentPosition*/ transform.position;
                    EndSphere.transform.position = /*CurrentPosition*/ transform.position;
                    agent.isStopped = true;
                    NPCMove = true;
                    yield break;
                }

                Vector3 sqrtDistance = transform.position - agent.destination;

                if (sqrtDistance.sqrMagnitude < 0.5f)
                {
                    OffMeshStartPosition = /*CurrentPosition*/ transform.position;
                    StartSphere.transform.position = /*CurrentPosition*/ transform.position;

                    OffMeshEndPosition = /*CurrentPosition*/ transform.position;
                    EndSphere.transform.position = /*CurrentPosition*/ transform.position;
                    NPCMove = true;
                    agent.isStopped = true;
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