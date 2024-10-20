using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndieGameDev
{
    [CustomEditor(typeof(PathFindingAgent))]
    public class PathFindingAgentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PathFindingAgent finderAgent = (PathFindingAgent)target;
            if (GUILayout.Button("Chase Target"))
            {
                finderAgent.GoToDistination();
            }
        }
    }
}