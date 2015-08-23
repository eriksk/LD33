using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Sequences
{
    public class StorySequence : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public List<TextSequence> Texts = new List<TextSequence>();
        public AudioClip BoatHornClip;

        public string GoToLevel;
        private bool _skipRequested = false;

        void Start()
        {
            StartCoroutine(Intro());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _skipRequested = true;
            }
        }

        private IEnumerator Intro()
        {
            Text.text = "";
            yield return new WaitForSeconds(0.5f);

            foreach (var textSequence in Texts)
            {
                PlaySound(BoatHornClip);
                var typeWriter = new TypeWriter(textSequence.Text, 80f);

                while (!typeWriter.Done)
                {
                    if (_skipRequested)
                    {
                        typeWriter.SkipToEnd();
                        _skipRequested = false;
                        break;
                    }

                    if (typeWriter.Update())
                    {
                        Text.text = typeWriter.CurrentText;
                    }
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(0.5f);
            }

            Text.text = "";

            // TODO: goto next scene
            if (!string.IsNullOrEmpty(GoToLevel))
            {
                Application.LoadLevel(GoToLevel);
            }
        }

        private void PlaySound(AudioClip audioClip)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
        }
    }
}
