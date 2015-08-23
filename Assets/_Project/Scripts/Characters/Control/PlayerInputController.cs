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
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                input.FirePrimary = true;
            if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                input.FireSecondary = true;
            if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                input.FireTertiary = true;

        }
    }
}
