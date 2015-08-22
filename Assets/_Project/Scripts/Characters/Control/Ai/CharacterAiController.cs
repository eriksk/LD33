using Assets._Project.Scripts.Characters.Input;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.Control.Ai
{
    [RequireComponent(typeof(CharacterInput))]
    public class CharacterAiController : MonoBehaviour
    {
        void Update()
        {
            var input = GetComponent<CharacterInput>();
            input.Clear();

            // TODO: stupid ai
        }
    }
}
