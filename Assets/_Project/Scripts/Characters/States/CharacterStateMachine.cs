using System;
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
        public CharacterStateMachineType Type = CharacterStateMachineType.Player;

        void Start()
        {
            _states = CharacterStateMachineFactory.Create(Type);

            foreach (var characterState in _states)
                characterState.Machine = this;

            _current = _states[0];
            _current.OnEnter();
        }

        public void Set<TState>() where TState : CharacterState
        {
            try
            {
                var state = _states.First(x => x is TState);
                _current.OnLeave();
                _current = state;
                _current.OnEnter();
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        void Update()
        {
            if (_current == null)
                return;

            _current.Update();
        }
    }
}
