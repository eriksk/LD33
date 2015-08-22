using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using Assets._Project.Scripts.Physics;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    [RequireComponent(typeof(CharacterFlip))]
    public class Melee : MonoBehaviour
    {
        public Vector2 Offset;
        public float Reach = 0.25f;
        public LayerMask HitMask;
        public int Damage = 1;
        public AudioClip Sound;
        public AudioClip HitSound;
        public bool ShowDebug = false;

        public void DoAttack()
        {
            PlaySound(Sound);
            var direction = new Vector2(GetComponent<CharacterFlip>().FlippedAsUnit, 0f);
            var hit = PsxExt.RayCastWithDebug(transform.position + new Vector3(Offset.x, Offset.y, 0f), direction, Reach, HitMask, ShowDebug);

            if (hit.collider != null)
            {
                var health = hit.collider.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    PlaySound(HitSound);
                    health.Deal(Damage);
                }
            }
        }

        private void PlaySound(AudioClip sound)
        {
            if (sound != null)
            {
                var audio = GetComponent<AudioSource>();
                if (audio == null)
                {
                    Debug.Log("No audio source for object " + gameObject.name);
                }
                else
                {
                    audio.PlayOneShot(sound);
                }
            }
        }
    }
}
