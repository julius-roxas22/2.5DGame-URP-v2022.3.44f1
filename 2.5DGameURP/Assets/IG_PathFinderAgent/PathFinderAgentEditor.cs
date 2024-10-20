using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndieGameDev
{
    [CustomEditor(typeof(PathFinderAgent))]
    public class PathFinderAgentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PathFinderAgent finderAgent = (PathFinderAgent)target;
            if (GUILayout.Button("Chase Target"))
            {
                finderAgent.TowardsToTarget();
            }
        }
    }
}