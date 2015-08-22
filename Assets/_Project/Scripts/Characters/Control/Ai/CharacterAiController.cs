using Assets._Project.Scripts.Characters.Collision;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Physics;
using Assets._Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

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
        private GameObject _player;

        public float SeeDistance = 2f;
        public LayerMask SeeMask;
        public bool DrawDebug = false;
        public float NearPlayerTreshold = 0.1f;

        void Start()
        {
            _surroundings = GetComponent<CharacterSurroundings>();
            _input = GetComponent<CharacterInput>();
            _flip = GetComponent<CharacterFlip>();
            _player = GameObject.Find("Player");
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
            if (_player == null)
            {
                if(_state != AiState.Wander)
                    SetState(AiState.Wander, 1000f);
            }

            switch (_state)
            {
                case AiState.Idle:
                    UpdateIdleState();
                    return;
                case AiState.Wander:
                    UpdateWanderState();
                    break;
                case AiState.Chase:
                    UpdateChaseState();
                    break;
                case AiState.Attack:
                    UpdateAttackState();
                    break;
            }
        }

        private void UpdateAttackState()
        {
            _input.FirePrimary = true;
        }

        private void UpdateIdleState()
        {
            if (CanSeePlayer())
            {
                SetState(AiState.Chase, 1000f);
            }
        }

        private void UpdateWanderState()
        {
            if (CanSeePlayer())
            {
                SetState(AiState.Chase, 1000f);
                return;
            }

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

        private void UpdateChaseState()
        {
            var direction = GetDirectionToPlayerAsXUnit();

            if (direction < 0f)
                _input.Left = true;
            if (direction > 0f)
                _input.Right = true;

            if (IsNearPlayer())
            {
                SetState(AiState.Attack, 100f);
            }
        }

        private bool IsNearPlayer()
        {
            return Mathf.Abs(transform.position.x - _player.transform.position.x) < NearPlayerTreshold;
        }

        private float GetDirectionToPlayerAsXUnit()
        {
            return transform.position.x < _player.transform.position.x ? 1f : -1f;
        }

        private bool CanSeePlayer()
        {
            if (_player == null)
                return false;

            var direction = new Vector2(_flip.FlippedAsUnit, 0f);

            var hit = PsxExt.RayCastWithDebug(transform.position + _surroundings.GetMiddleAsVector3(), direction, SeeDistance, SeeMask, DrawDebug);
            if (hit.collider == null) return false;
            if (hit.collider.gameObject.name != "Player") return false;
            return hit.collider.gameObject.GetComponent<Health>().Alive;
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
