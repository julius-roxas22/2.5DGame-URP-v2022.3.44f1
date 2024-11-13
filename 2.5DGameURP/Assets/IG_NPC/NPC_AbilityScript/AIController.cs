using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum AI_TYPE
    {
        WALK_N_JUMP,
        RUN
    }

    public class AIController : MonoBehaviour
    {
        public List<AISubset> AISubsets = new List<AISubset>();
        public AI_TYPE InitialAI;

        private void Awake()
        {
            AISubset[] arr = gameObject.GetComponentsInChildren<AISubset>();

            foreach (AISubset a in arr)
            {
                if (!AISubsets.Contains(a))
                {
                    AISubsets.Add(a);
                    a.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            TriggerAI(InitialAI);
        }

        public void TriggerAI(AI_TYPE AiType)
        {
            AISubset next = null;

            foreach (AISubset a in AISubsets)
            {
                a.gameObject.SetActive(false);
                if (AiType == a.AIType)
                {
                    next = a;
                }
            }
            next.gameObject.SetActive(true);
        }
    }
}

