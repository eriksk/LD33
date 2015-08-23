using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Jukebox : MonoBehaviour
    {
        private static Jukebox instance = null;
        public static Jukebox Instance
        {
            get { return instance; }
        }

        void OnLevelWasLoaded(int level)
        {
            var source = GetComponent<AudioSource>();
            if (source == null)
                return;

            if (!source.isPlaying)
                source.Play();
        }

        void Awake()
        {
            if (instance != null && instance != this) {
                Destroy(gameObject);
                return;
            } else {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
