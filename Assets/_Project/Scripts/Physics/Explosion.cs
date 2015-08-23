using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Cameras;
using Assets._Project.Scripts.Characters;
using Assets._Project.Scripts.Characters.Movement;
using UnityEngine;

namespace Assets._Project.Scripts.Physics
{
    [ExecuteInEditMode]
    public class Explosion : MonoBehaviour
    {
        public LayerMask LayerMask;
        public float Radius = 1f;
        public float Force = 10f;
        public float UpForce = 0f;
        public int Damage = 1;

        void Start()
        {
            Explode();
        }

        void Update()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(Radius, 0f, 0f), Color.yellow, 0.001f);
        }

        public void Explode()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask);

            var position = transform.position;

            foreach (var collider in colliders)
            {
                var rigidBody = collider.gameObject.GetComponent<Rigidbody2D>();
                if (rigidBody == null) continue;

                var movement = collider.gameObject.GetComponent<CharacterMovement>();
                if (movement != null)
                {
                    movement.DisableForSeconds(1);
                }
                var objPosition = collider.gameObject.transform.position;

                float distance = Vector2.Distance(objPosition, position) / Radius;
                float angle = Mathf.Atan2(objPosition.y - position.y, objPosition.x - position.x);

                var force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Force;
                force.y += UpForce;

                rigidBody.AddForce(force * (1f - distance), ForceMode2D.Impulse);

                var health = collider.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.Deal(Damage);
                }

            }

            ShakeCamera();
        }

        private void ShakeCamera()
        {
            Camera.main.GetComponent<CamShake>().Shake(500f, Force);
        }
    }
}
