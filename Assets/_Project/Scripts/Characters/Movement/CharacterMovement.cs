using System;
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

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _surroundings = GetComponent<CharacterSurroundings>();
        }

        void FixedUpdate()
        {
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
            var vel = _rigidbody.velocity;
            vel.x = 0f;
            _rigidbody.velocity = vel;
        }

        public void Jump()
        {
            var vel = _rigidbody.velocity;
            vel.y = JumpForce;
            _rigidbody.velocity = vel;
        }

        //public void StopYIfMovingUpwards()
        //{
        //    var vel = _rigidbody.velocity;
        //    if (vel.y > 0f)
        //        vel.y = 0f;
        //    _rigidbody.velocity = vel;
        //}
    }
}
