using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Characters.InputControl;
using Assets._Project.Scripts.Characters.Movement;
using Assets._Project.Scripts.Characters.States;
using Assets._Project.Scripts.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Transitioning
{
    public class ChangeSceneTrigger : MonoBehaviour
    {
        public string TargetLevel;
        private bool _trigged = false;
        public UnityEngine.UI.Text Text;

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

            if (Text != null)
            {
                var typewriter = new TypeWriter("Level cleared!", 100f);
                while (!typewriter.Done)
                {
                    if (typewriter.Update())
                    {
                        Text.text = typewriter.CurrentText;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return new WaitForSeconds(2);
            Application.LoadLevel(TargetLevel);
        }
    }
}
