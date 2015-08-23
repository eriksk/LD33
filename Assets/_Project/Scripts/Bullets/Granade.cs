using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using UnityEngine;

namespace Assets._Project.Scripts.Bullets
{
    public class Granade : MonoBehaviour
    {
        public GameObject Explosion;
        public float LifeTime = 1f;

        private void Start()
        {
            StartCoroutine(StartTicking());
        }

        private IEnumerator StartTicking()
        {
            yield return new WaitForSeconds(LifeTime);
            CreateExplosion();

            // make it invisible
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<SpriteRenderer>());

            var particles = GetComponent<ParticleSystem>();
            particles.Stop();

            while (particles.IsAlive())
                yield return new WaitForEndOfFrame();

            Destroy(gameObject);
        }

        private void CreateExplosion()
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
}