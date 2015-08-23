using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Audio
{
    [Serializable]
    public class AudioCollection
    {
        public List<AudioCollectionEntry> Entries = new List<AudioCollectionEntry>(); 

        public AudioClip Get(string name)
        {
            foreach (var entry in Entries)
            {
                if (entry.Name == name)
                    return entry.GetRandomClip();
            }
            return null;
        }
    }

    [Serializable]
    public class AudioCollectionEntry
    {
        public List<AudioClip> Clips = new List<AudioClip>();
        public string Name;

        public AudioClip GetRandomClip()
        {
            return Clips[Random.Range(0, Clips.Count)];
        }
    }
}
