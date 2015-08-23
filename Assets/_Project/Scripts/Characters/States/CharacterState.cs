using Assets._Project.Scripts.Animations;
using Assets._Project.Scripts.Audio;
using Assets._Project.Scripts.Characters.Collision;
using Assets._Project.Scripts.Characters.Input;
using Assets._Project.Scripts.Characters.Movement;
using Assets._Project.Scripts.Weapons;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.States
{
    public class CharacterState
    {
        public CharacterStateMachine Machine { get; set; }

        public void Set<TState>() where TState : CharacterState
        {
            Machine.Set<TState>();
        }

        protected GameObject GameObject
        {
            get { return Machine.gameObject; }
        }

        protected CharacterMovement Movement
        {
            get { return Machine.gameObject.GetComponent<CharacterMovement>(); }
        }

        protected CharacterInput Input
        {
            get { return Machine.gameObject.GetComponent<CharacterInput>(); }
        }

        protected CharacterSurroundings Surroundings
        {
            get { return Machine.gameObject.GetComponent<CharacterSurroundings>(); }
        }

        protected SpriteAnimations Animations
        {
            get { return Machine.gameObject.GetComponent<SpriteAnimations>(); }
        }

        protected Weapon Weapon
        {
            get { return Machine.gameObject.GetComponent<Weapon>(); }
        }

        protected CharacterFlip Flip
        {
            get { return Machine.gameObject.GetComponent<CharacterFlip>(); }
        }

        protected Melee Melee
        {
            get { return Machine.gameObject.GetComponent<Melee>(); }
        }

        protected AudioCollection AudioCollection
        {
            get { return Machine.gameObject.GetComponent<CharacterDefinition>().AudioCollection; }
        }

        protected void PlaySound(AudioClip clip)
        {
            Machine.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnLeave()
        {
        }

        public virtual void Update()
        {
        }
    }
}