using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters.Collision;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterSurroundings))]
    public class CharacterMovement : MonoBehaviour
    {
        public float Acceleration = 1f;
        public float MaxSpeed = 3f;
        public float JumpForce = 2f;
        public float HorizontalDamping = 0.01f;

        private Rigidbody2D _rigidbody;
        private CharacterSurroundings _surroundings;
        private float _linearDrag;

        private bool _disabled;

        void Start()
        {
            _disabled = false;
            _rigidbody = GetComponent<Rigidbody2D>();
            _surroundings = GetComponent<CharacterSurroundings>();
            _linearDrag = _rigidbody.drag;
            _rigidbody.drag = 0f;
        }

        void FixedUpdate()
        {
            if (_disabled)
                return;

            var velocity = _rigidbody.velocity;

            if (velocity.x > 0f)
            {
                velocity.x -= HorizontalDamping;
                if (velocity.x < 0f)
                    velocity.x = 0;
            }
            if (velocity.x < 0f)
            {
                velocity.x += HorizontalDamping;
                if (velocity.x > 0f)
                    velocity.x = 0;
            }

            _rigidbody.velocity = velocity;
        }

        public void MoveLeft()
        {
            if (_disabled)
                return;

            if (_surroundings.Left)
                return;

            var vel = _rigidbody.velocity;

            vel.x -= Acceleration;
            if (vel.x < -MaxSpeed)
                vel.x = -MaxSpeed;

            _rigidbody.velocity  = vel;
        }

        public void MoveRight()
        {
            if (_disabled)
                return;

            if (_surroundings.Right)
                return;

            var vel = _rigidbody.velocity;

            vel.x += Acceleration;
            if (vel.x > MaxSpeed)
                vel.x = MaxSpeed;
            _rigidbody.velocity = vel;
        }

        public void Stop()
        {
            if (_disabled)
                return;

            var vel = _rigidbody.velocity;
            vel.x = 0f;
            _rigidbody.velocity = vel;
        }

        public void Jump()
        {
            if (_disabled)
                return;

            var vel = _rigidbody.velocity;
            vel.y = JumpForce;
            _rigidbody.velocity = vel;
        }
        
        public void DisableForSeconds(float seconds)
        {
            StartCoroutine(Disable(seconds));
        }

        private IEnumerator Disable(float seconds)
        {
            if (_rigidbody != null)
            {
                _rigidbody.drag = _linearDrag;
                _disabled = true;
                yield return new WaitForSeconds(0.1f);
                int maxTries = (int) (seconds*1000);
                int tries = 0;
                // allow enable if grounded again
                while (!_surroundings.Down || tries >= maxTries)
                {
                    tries++;
                    yield return new WaitForEndOfFrame();
                }
                _disabled = false;
                _rigidbody.drag = 0f;
            }
        }
    }
}
