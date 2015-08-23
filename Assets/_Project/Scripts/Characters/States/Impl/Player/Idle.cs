using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.States.Impl.Player
{
    public class Idle : CharacterState
    {
        public override void OnEnter()
        {
            Animations.SetAnim("idle");
            base.OnEnter();
        }

        public override void Update()
        {
            if (Input.FirePrimary)
            {
                Weapon.Fire(Mathf.Deg2Rad * (Flip.Flipped ? 180f : 0f));
            }
            if (Input.FireSecondary)
            {
                // TODO: throw granade
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
        public override void OnEnter()
        {
            Animations.SetAnim("walk");
            Animations.OnFrameEnter += AnimationsOnOnFrameEnter;
            base.OnEnter();
        }

        private void AnimationsOnOnFrameEnter(int frameIndex, int frameValue)
        {
            if (frameIndex == 0)
            {
                PlaySound(AudioCollection.Get("step_left"));
            }
            if (frameIndex == 3)
            {
                PlaySound(AudioCollection.Get("step_right"));
            }
        }

        public override void OnLeave()
        {
            Animations.OnFrameEnter -= AnimationsOnOnFrameEnter;
            base.OnLeave();
        }

        public override void Update()
        {

            if (Input.FirePrimary)
            {
                Weapon.Fire(Mathf.Deg2Rad * (Flip.Flipped ? 180f : 0f));
            }
            if (Input.FireSecondary)
            {
                // TODO: throw granade
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

    public class Jump : CharacterState
    {
        public override void OnEnter()
        {
            PlaySound(AudioCollection.Get("jump"));
            Animations.SetAnim("jump");
            Movement.Jump();
            base.OnEnter();
        }

        public override void Update()
        {
            if (Input.FirePrimary)
            {
                Weapon.Fire(Mathf.Deg2Rad * (Flip.Flipped ? 180f : 0f));
            }
            if (Input.FireSecondary)
            {
                // TODO: throw granade
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
