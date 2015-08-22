using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Assets._Project.Scripts.Bullets;
using UnityEngine;

namespace Assets._Project.Scripts.Characters
{
    // TODO: All required types here :D
    [RequireComponent(typeof(Health))]
    public class CharacterDefinition : MonoBehaviour
    {
        public GameObject BulletHitEffectPrefab;
        public GameObject OnDeathEffectPrefab;

        // TODO: default properties for character, capabilities etc, button for configuring it?

        void OnTriggerEnter2D(Collider2D coll)
        {
            //var bullet = coll.gameObject.GetComponent<Bullet>();
            //if (bullet != null)
            //    GetComponent<Health>().Deal(bullet.Damage);

            if (BulletHitEffectPrefab != null)
                Instantiate(BulletHitEffectPrefab, transform.position, transform.rotation);

            CheckDeath();
        }

        void OnCollision2D(Collision2D coll)
        {
            //var bullet = coll.gameObject.GetComponent<Bullet>();
            //if (bullet != null)
            //    GetComponent<Health>().Deal(bullet.Damage);

            if (BulletHitEffectPrefab != null)
                Instantiate(BulletHitEffectPrefab, coll.contacts[0].point, transform.rotation);

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (GetComponent<Health>().Dead)
            {
                if (OnDeathEffectPrefab != null)
                {
                    Instantiate(OnDeathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
