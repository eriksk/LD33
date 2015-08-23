using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Sequences
{
    public class IntroSequence : MonoBehaviour
    {
        public UnityEngine.UI.Text Header;
        public UnityEngine.UI.Text SubHeader;

        public AudioClip AudioC;
        public AudioClip AudioE;
        public AudioClip AudioO;
        public AudioClip AudioChief;
        public AudioClip AudioExecution;
        public AudioClip AudioOfficer;

        void Start()
        {
            if (Header == null)
                throw new ArgumentException("Header is null");
            if (SubHeader == null)
                throw new ArgumentException("SubHeader is null");


            StartCoroutine(Intro());
        }

        private IEnumerator Intro()
        {
            var ceoSequence = new[]
            {
                AudioC,
                AudioE,
                AudioO,
            };

            var chiefExecutionOfficerSequence = new[]
            {
                AudioChief,
                AudioExecution,
                AudioOfficer,
            };

            var headerTypeWriter = new TypeWriter(Header.text, 500);
            Header.text = headerTypeWriter.CurrentText;

            var subHeaderTypeWriter = new TypeWriter(SubHeader.text, 1000, true);
            SubHeader.text = subHeaderTypeWriter.CurrentText;

            yield return new WaitForSeconds(1f);

            int soundIndex = 0;
            while (!headerTypeWriter.Done)
            {
                if (headerTypeWriter.Update())
                {
                    PlaySound(ceoSequence[soundIndex++]);
                    Header.text = headerTypeWriter.CurrentText;
                }

                yield return new WaitForEndOfFrame();
            }

            soundIndex = 0;
            while (!subHeaderTypeWriter.Done)
            {
                if (subHeaderTypeWriter.Update())
                {
                    PlaySound(chiefExecutionOfficerSequence[soundIndex++]);
                    SubHeader.text = subHeaderTypeWriter.CurrentText;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void PlaySound(AudioClip audioClip)
        {
            var source = GetComponent<AudioSource>();
            source.PlayOneShot(audioClip);
        }
    }
}
