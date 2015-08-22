using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Props
{
    public class FireExtinguisher : DestroyableProp
    {
        public Vector2 Velocity = new Vector2();
        public ParticleSystem ThrusterParticleSystem;
        public AudioClip ThrusterNoiseAudio;

        private bool _thrusting = false;

        public override void Start()
        {
            if (ThrusterParticleSystem != null)
                ThrusterParticleSystem.Stop();
            base.Start();
        }

        void Update()
        {
            if (_thrusting)
            {
                GetComponent<Rigidbody2D>().AddRelativeForce(Velocity);
            }
        }

        protected override IEnumerator DelayedDestroy()
        {
            BeginThrust();
            yield return new WaitForSeconds(DestroyDelay);
            EndThrust();
            Destroy();
        }

        private void EndThrust()
        {

            if (ThrusterNoiseAudio != null)
            {
                var audio = GetComponent<AudioSource>();
                if (audio == null)
                {
                    Debug.Log("No audio source for object " + gameObject.name);
                }
                else
                {
                    audio.Stop();
                }
            }
        }

        private void BeginThrust()
        {
            _thrusting = true;
            if (ThrusterParticleSystem != null)
                ThrusterParticleSystem.Play();


            if (ThrusterNoiseAudio != null)
            {
                var audio = GetComponent<AudioSource>();
                if (audio == null)
                {
                    Debug.Log("No audio source for object " + gameObject.name);
                }
                else
                {
                    audio.clip = ThrusterNoiseAudio;
                    audio.Play();
                }
            }
        }
    }
}
