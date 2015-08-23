using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Spawning
{
    public class SpawnerWithVelocity : Spawner
    {
        public float Velocity = 0.1f;

        protected override void SpawnObject(GameObject obj)
        {
            var angle = Random.Range(0f, Mathf.PI*2f);
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            var o = (GameObject)Instantiate(obj, transform.position, transform.rotation);

            var rigidBody = o.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = dir * Velocity;
            }
        }
    }
}
