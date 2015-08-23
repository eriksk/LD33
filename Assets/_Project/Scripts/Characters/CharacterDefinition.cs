using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Audio;
using Assets._Project.Scripts.Characters.Control;
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

        public AudioCollection AudioCollection = new AudioCollection();

        void Start()
        {
            GetComponent<Health>().OnDeath += OnDeath;
        }

        private void OnDeath(GameObject obj)
        {
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

        public void DisableControl()
        {
            GetComponent<CharacterInput>().Clear();
            GetComponent<CharacterInputController>().enabled = false;
        }
    }
}
