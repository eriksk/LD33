using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Cameras
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class CamFollow : MonoBehaviour
    {
        public float dampTime = 0.15f;
        private Vector3 velocity = Vector3.zero;
        public Transform target;
        public Vector2 Offset = new Vector2();

        void Update()
        {
            if (target)
            {
                var cam = GetComponent<Camera>();
                Vector3 point = cam.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)) + new Vector3(Offset.x, Offset.y, 0f); 
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }

        }
    }
}
