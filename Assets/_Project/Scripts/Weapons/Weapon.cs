using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    [RequireComponent(typeof(CharacterFlip))]
    public class Weapon : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public Vector2 MuzzleOffset;
        public float Force = 2f;
        public float RecoilForce = 0.5f;

        public AudioClip FireSound;

        public float Interval = 100f;
        private float _current;

        public void Fire(float angle)
        {
            _current += Time.deltaTime*1000f;
            if (_current < Interval)
                return;
            _current = 0f;

            FireBullet(angle);
            ApplyRecoil();
        }

        private void FireBullet(float angle)
        {
            if (FireSound != null)
            {
                var audio = GetComponent<AudioSource>();
                if (audio == null)
                {
                    Debug.Log("No audio source for object " + gameObject.name);
                }
                else
                {
                    audio.PlayOneShot(FireSound);
                }
            }

            var pos = transform.position;
            pos.x += MuzzleOffset.x * transform.localScale.x * GetComponent<CharacterFlip>().FlippedAsUnit;
            pos.y += MuzzleOffset.y * transform.localScale.y;
            
            var bullet = (GameObject)Instantiate(BulletPrefab, pos, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
            var rigidBody = bullet.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Force;
        }

        private void ApplyRecoil()
        {
            // TODO: THis does not work because we set the velocity in the CharacterMovement script.
            var force = new Vector2(-GetComponent<CharacterFlip>().FlippedAsUnit, 0f) * RecoilForce;
            var rigidBody = GetComponent<Rigidbody2D>();
            var vel = rigidBody.velocity;
            vel += force;
            rigidBody.velocity = vel;
        }
    }
}
