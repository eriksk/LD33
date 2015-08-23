using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Spawning
{
    public class Spawner : MonoBehaviour
    {
        public List<Spawn>  Objects = new List<Spawn>();

        void Start()
        {
            foreach (var o in Objects)
            {
                if (o != null && o.Obj != null)
                {
                    for (int i = 0; i < o.Count; i++)
                    {
                        SpawnObject(o.Obj);
                    }
                }
            }
        }

        protected virtual void SpawnObject(GameObject obj)
        {
            Instantiate(obj, transform.position, transform.rotation);
        }
    }

    [Serializable]
    public class Spawn
    {
        public GameObject Obj;
        public int Count = 1;
    }
}
