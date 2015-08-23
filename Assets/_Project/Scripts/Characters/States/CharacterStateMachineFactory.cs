using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Project.Scripts.Characters.States
{
    public class CharacterStateMachineFactory
    {
        public static CharacterState[] Create(CharacterStateMachineType type)
        {
            switch (type)
            {
                case CharacterStateMachineType.Player:
                    return new CharacterState[]
                    {
                        new Impl.Player.Idle(),
                        new Impl.Player.Walk(),
                        new Impl.Player.Jump()
                    };

                case CharacterStateMachineType.Deamon:
                    return new CharacterState[]
                    {
                        new Impl.Enemy.Idle(),
                        new Impl.Enemy.Walk(),
                        new Impl.Enemy.Jump(),
                        new Impl.Enemy.AttackMelee(), 
                    };

                case CharacterStateMachineType.Mutant:
                    return new CharacterState[]
                    {
                        new Impl.Enemy.Idle(),
                        new Impl.Enemy.Walk(),
                        new Impl.Enemy.Jump(),
                        new Impl.Enemy.AttackWeapon()
                    };
            }
            
            throw new NotImplementedException(type.ToString());
        }
    }

    public enum CharacterStateMachineType
    {
        Player,
        Deamon,
        Mutant
    }
}
