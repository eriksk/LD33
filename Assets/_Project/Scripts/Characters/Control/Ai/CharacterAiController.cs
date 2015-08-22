using Assets._Project.Scripts.Characters.Collision;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Utils;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.Control.Ai
{
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(CharacterSurroundings))]
    [RequireComponent(typeof(CharacterFlip))]
    public class CharacterAiController : MonoBehaviour
    {
        private AiState _state = AiState.Idle;
        private readonly TimerTrig _timer = new TimerTrig(100f);
        private CharacterSurroundings _surroundings;
        private CharacterInput _input;
        private CharacterFlip _flip;

        void Start()
        {
            _surroundings = GetComponent<CharacterSurroundings>();
            _input = GetComponent<CharacterInput>();
            _flip = GetComponent<CharacterFlip>();
        }

        void Update()
        {
            _input.Clear();


            // TODO: stupid ai
            if (_timer.IsTrigged(Time.deltaTime*1000f))
            {
                SetState(GetRandomState(), Random.Range(500f, 5000f));
            }

            UpdateState();
        }

        private void UpdateState()
        {
            switch (_state)
            {
                case AiState.Idle:
                    return;
                case AiState.Wander:
                    UpdateWanderState();
                    break;
            }
        }

        private void UpdateWanderState()
        {
            var movingLeft = _flip.Flipped;
            var moveDirection = movingLeft;

            if (movingLeft)
            {
                if (_surroundings.Left)
                    moveDirection = false;
            }
            else
            {
                if (_surroundings.Right)
                    moveDirection = true;
            }

            if (moveDirection)
                _input.Left = true;
            else
                _input.Right = true;
        }

        private AiState GetRandomState()
        {
            var r = Random.Range(0, 2);
            if (r == 0)
                return AiState.Idle;
            return AiState.Wander;
        }

        private void SetState(AiState state, float duration)
        {
            _state = state;
            _timer.Set(duration);
        }
    }

    internal enum AiState
    {
        Idle,
        Wander,
        Chase,
        Attack
    }
}
