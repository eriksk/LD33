using System.Collections;
using System.Collections.Generic;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Sequences
{
    public class OutroSequence : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public List<TextSequence> Texts = new List<TextSequence>();
        public AudioClip BoatHornClip;

        private bool _skipRequested = false;
        public string GoToLevel;

        void Start()
        {
            StartCoroutine(Outro());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _skipRequested = true;
            }
        }

        private IEnumerator Outro()
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

            yield return new WaitForSeconds(2f);

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
