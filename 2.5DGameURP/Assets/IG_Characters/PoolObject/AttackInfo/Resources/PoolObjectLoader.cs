using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum PoolObjectType
    {
        ATTACKINFO,
        HAMMER,
        HAMMERDOWN_VFX
    }
    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiateObject(PoolObjectType objectType)
        {
            GameObject obj = null;
            switch (objectType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
                        break;
                    }
                case PoolObjectType.HAMMER:
                    {
                        obj = Instantiate(Resources.Load("Axe", typeof(GameObject))) as GameObject;
                        break;
                    }
                case PoolObjectType.HAMMERDOWN_VFX:
                    {
                        obj = Instantiate(Resources.Load("VFX_HammerDown", typeof(GameObject))) as GameObject;
                        break;
                    }
            }
            return obj.GetComponent<PoolObject>();
        }
    }

}
