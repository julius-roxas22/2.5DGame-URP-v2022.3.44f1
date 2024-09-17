using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CameraManager : Singleton<CameraManager>
    {
        private CameraController CamController;
        private Coroutine routine;

        public CameraController GetCamController
        {
            get
            {
                if (null == CamController)
                {
                    CamController = FindObjectOfType<CameraController>();
                }
                return CamController;
            }
        }

        private IEnumerator IECamShake(float duration)
        {
            GetCamController.TriggerCamera(CameraTrigger.Shake);
            yield return new WaitForSeconds(duration);
            GetCamController.TriggerCamera(CameraTrigger.Default);
        }

        public void ShakeCamera(float duration)
        {
            if(null != routine)
            {
                StopCoroutine(routine);
            }
            routine = StartCoroutine(IECamShake(duration));
        }
    }
}