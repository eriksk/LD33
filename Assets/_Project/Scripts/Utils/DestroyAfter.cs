using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Utils
{
    public class DestroyAfter : MonoBehaviour
    {
        public float Time = 1f;

        void Start()
        {
            Destroy(gameObject, Time);
        }
    }
}
