using UnityEngine;

namespace Assets._Project.Scripts.Characters.Input
{
    public class CharacterInput : MonoBehaviour
    {
        public bool Left, Right, Up, Down, Jump, FirePrimary;

        public void Clear()
        {
            Left = false;
            Right = false;
            Up = false;
            Down = false;
            Jump = false;
            FirePrimary = false;
        }
    }
}
