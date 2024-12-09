using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class Settings : MonoBehaviour
    {
        public FrameSettings frameSettings;

        private void Awake()
        {
            Time.timeScale = frameSettings.TimeScale;
            Application.targetFrameRate = frameSettings.TargetFps;
        }
    }
}

