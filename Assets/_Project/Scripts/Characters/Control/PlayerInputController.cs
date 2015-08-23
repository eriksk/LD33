using Assets._Project.Scripts.Characters.Control;
using Assets._Project.Scripts.Characters.Input;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.InputControl
{
    [RequireComponent(typeof(CharacterInput))]
    public class PlayerInputController : CharacterInputController
    {
        void Update()
        {
            var input = GetComponent<CharacterInput>();
            input.Clear();


            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                input.Left = true;
            if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                input.Right = true;
            if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
                input.Jump = true;
            if (UnityEngine.Input.GetKey(KeyCode.Space))
                input.FirePrimary = true;

        }
    }
}
