using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySoundOnImpact : MonoBehaviour
    {
        public float TresholdForImpact = 0.01f;
        public List<AudioClip> AudioClips = new List<AudioClip>();

        private AudioSource _source;

        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            var force = coll.relativeVelocity.sqrMagnitude;

            if (force > TresholdForImpact)
            {
                PlayRandomSound();
            }
        }

        private void PlayRandomSound()
        {
            if (AudioClips.Count == 0)
                return;

            if(_source != null)
                _source.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Count)]);
        }
    }
}
