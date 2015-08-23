using System;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.States.Impl.Enemy
{
    public class Idle : CharacterState
    {
        private readonly Type _attackType;

        public Idle(Type attackType)
        {
            _attackType = attackType;
        }

        public override void OnEnter()
        {
            Animations.SetAnim("idle");
            base.OnEnter();
        }

        public override void Update()
        {
            if (Input.FirePrimary)
            {
                Set(_attackType);
                return;
            }

            if (Input.Left || Input.Right)
            {
                Set<Walk>();
                return;
            }
            else
            {
                Movement.Stop();
            }

            if (Input.Jump)
            {
                Set<Jump>();
                return;
            }

            base.Update();
        }
    }

    public class Walk : CharacterState
    {
        private readonly Type _attackType;

        public Walk(Type attackType)
        {
            _attackType = attackType;
        }

        public override void OnEnter()
        {
            Animations.SetAnim("walk");
            base.OnEnter();
        }

        public override void Update()
        {

            if (Input.FirePrimary)
            {
                Set(_attackType);
                return;
            }


            if (Input.Jump)
            {
                Set<Jump>();
                return;
            }

            if (Input.Left)
            {
                Movement.MoveLeft();
                Flip.FlipLeft();
            }
            else if (Input.Right)
            {
                Movement.MoveRight();
                Flip.FlipRight();
            }
            else
            {
                Movement.Stop();
                Set<Idle>();
                return;
            }

            base.Update();
        }
    }

    public abstract class Attack : CharacterState
    {
    }


    public class AttackMelee : CharacterState
    {
        public override void OnEnter()
        {
            Animations.SetAnim("attack");
            Animations.OnAnimationEnd += OnAnimationEnd;
            Animations.OnFrameEnter += OnFrameEnter;
            Movement.Stop();
            base.OnEnter();
        }

        private void OnFrameEnter(int frameIndex, int frameValue)
        {
            if (frameIndex == 2)
                Melee.DoAttack();
        }

        private void OnAnimationEnd()
        {
            Set<Idle>();
        }

        public override void OnLeave()
        {
            Animations.OnAnimationEnd -= OnAnimationEnd;
            Animations.OnFrameEnter -= OnFrameEnter;
            base.OnLeave();
        }

        public override void Update()
        {
            Movement.Stop();

            base.Update();
        }
    }

    public class AttackWeapon : Attack
    {
        public override void OnEnter()
        {
            Animations.SetAnim("attack");
            Animations.OnAnimationEnd += OnAnimationEnd;
            Animations.OnFrameEnter += OnFrameEnter;
            Movement.Stop();
            base.OnEnter();
        }

        private void OnFrameEnter(int frameIndex, int frameValue)
        {
            if (frameIndex == 3)
                Weapon.Fire(Mathf.Deg2Rad * (Flip.FlippedAsUnit < 0f ? 180f : 0f));
        }

        private void OnAnimationEnd()
        {
            Set<Idle>();
        }

        public override void OnLeave()
        {
            Animations.OnAnimationEnd -= OnAnimationEnd;
            Animations.OnFrameEnter -= OnFrameEnter;
            base.OnLeave();
        }

        public override void Update()
        {
            Movement.Stop();

            base.Update();
        }
    }

    public class Jump : CharacterState
    {
        private Type _attackType;

        public Jump(Type attackType)
        {
            _attackType = attackType;
        }

        public override void OnEnter()
        {
            Animations.SetAnim("jump");
            Movement.Jump();
            base.OnEnter();
        }

        public override void Update()
        {

            if (Input.FirePrimary)
            {
                Set(_attackType);
                return;
            }

            if (Input.Left)
            {
                Movement.MoveLeft();
                Flip.FlipLeft();
            }
            else if (Input.Right)
            {
                Movement.MoveRight();
                Flip.FlipRight();
            }
            else
            {
                Movement.Stop();
            }


            if (Surroundings.Down)
            {
                Set<Idle>();
                return;
            }


            base.Update();
        }
    }
}
