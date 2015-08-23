using System.Collections;
using Assets._Project.Scripts.Characters;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Transitioning
{
    public class ChangeSceneTrigger : MonoBehaviour
    {
        public string TargetLevel;
        private bool _trigged = false;
        public UnityEngine.UI.Text Text;
        public AudioClip LevelClearedClip;
        public AudioClip LevelClearedSongClip;

        void Start()
        {
            Text.text = "";
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if(_trigged)
                return;

            if (string.IsNullOrEmpty(TargetLevel))
                return;

            if (coll.gameObject.name != "Player")
                return;

            _trigged = true;
            StartCoroutine(EndLevel());
        }

        private IEnumerator EndLevel()
        {
            var player = GameObject.Find("Player");
            if (player != null)
            {
                player.GetComponent<CharacterDefinition>().DisableControl();
            }

            GameObject.Find("Jukebox").GetComponent<AudioSource>().Stop();

            GetComponent<AudioSource>().PlayOneShot(LevelClearedSongClip);
            if (Text != null)
            {
                var typewriter = new TypeWriter("Level cleared!", 60f);
                while (!typewriter.Done)
                {
                    if (typewriter.Update())
                    {
                        Text.text = typewriter.CurrentText;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            GetComponent<AudioSource>().PlayOneShot(LevelClearedClip);

            yield return new WaitForSeconds(2);
            Application.LoadLevel(TargetLevel);
        }
    }
}
