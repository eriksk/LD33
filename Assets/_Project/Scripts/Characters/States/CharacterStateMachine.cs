using System.Linq;
using Assets._Project.Scripts.Characters.Collision;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Characters.Movement;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.States
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterSurroundings))]
    [RequireComponent(typeof(CharacterFlip))]
    public class CharacterStateMachine : MonoBehaviour
    {
        private CharacterState[] _states;
        private CharacterState _current;

        void Start()
        {
            _states = new CharacterState[]
            {
                new Impl.Player.Idle(),
                new Impl.Player.Walk(), 
                new Impl.Player.Jump()
            };

            foreach (var characterState in _states)
                characterState.Machine = this;

            _current = _states[0];
            _current.OnEnter();
        }

        public void Set<TState>() where TState : CharacterState
        {
            var state = _states.First(x => x.GetType() == typeof (TState));
            _current.OnLeave();
            _current = state;
            _current.OnEnter();
        }

        void Update()
        {
            if (_current == null)
                return;

            _current.Update();
        }
    }
}
