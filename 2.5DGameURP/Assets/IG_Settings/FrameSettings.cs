using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New FrameSettings", menuName = "IndieGameDev/Settings/FrameSettings")]
    public class FrameSettings : ScriptableObject
    {
        [Range(0.01f, 1f)]
        public float TimeScale;
        public int TargetFps;
    }

}
