using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Transitioning
{
    public class RestartSceneOnDeath : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public GameObject Character;
        private bool _skipRequested;
        public AudioClip YouDiedClip;

        void Start()
        {
            _skipRequested = false;
            Character.GetComponent<Health>().OnDeath += OnOnDeath;
        }

        private void OnOnDeath(GameObject gameObject)
        {
            _skipRequested = false;
            StartCoroutine(DelayRestart());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _skipRequested = true;
            }
        }

        private IEnumerator DelayRestart()
        {
            var player = GameObject.Find("Player");
            if (player != null)
            {
                player.GetComponent<CharacterDefinition>().DisableControl();
            }

            if (Text != null)
            {
                GetComponent<AudioSource>().PlayOneShot(YouDiedClip);
                var typewriter = new TypeWriter("You died!", 50f);
                while (!typewriter.Done)
                {
                    if (_skipRequested)
                    {
                        typewriter.SkipToEnd();
                        Text.text = typewriter.CurrentText;
                        break;
                    }

                    if (typewriter.Update())
                    {
                        Text.text = typewriter.CurrentText;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
            if(!_skipRequested)
                yield return new WaitForSeconds(2);

            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
