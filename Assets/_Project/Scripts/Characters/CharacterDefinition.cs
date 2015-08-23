using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Characters.InputControl;
using UnityEngine;

namespace Assets._Project.Scripts.Characters
{
    [RequireComponent(typeof(Health))]
    public class CharacterDefinition : MonoBehaviour
    {
        public GameObject BulletHitEffectPrefab;
        public GameObject OnDeathEffectPrefab;

        void Start()
        {
            GetComponent<Health>().OnDeath += OnDeath;
        }

        private void OnDeath(GameObject obj)
        {
            CheckDeath();
        }

        //void OnTriggerEnter2D(Collider2D coll)
        //{
        //    //if (BulletHitEffectPrefab != null)
        //    //    Instantiate(BulletHitEffectPrefab, transform.position, transform.rotation);

        //    //CheckDeath();
        //}

        //void OnCollision2D(Collision2D coll)
        //{
        //    //if (BulletHitEffectPrefab != null)
        //    //    Instantiate(BulletHitEffectPrefab, coll.contacts[0].point, transform.rotation);

        //    //CheckDeath();
        //}

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

        public void DisableControl()
        {
            GetComponent<CharacterInput>().Clear();
            GetComponent<PlayerInputController>().enabled = false;
        }
    }
}
