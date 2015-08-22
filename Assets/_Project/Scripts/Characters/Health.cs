using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Characters
{
    public class Health : MonoBehaviour
    {
        public int Initial = 100;
        private int _health;

        public delegate void Death(GameObject gameObject);
        public event Death OnDeath;

        void Start()
        {
            _health = Initial;
        }

        public float Unit
        {
            get
            {
                return (_health/(float) Initial);
            }
        }

        public float Percent
        {
            get { return (int)(Unit*100f); }
        }

        public bool Alive
        {
            get { return _health > 0; }
        }

        public bool Dead
        {
            get { return !Alive; }
        }

        public void Deal(int damage)
        {
            _health -= damage;
            Clamp();

            if (Dead && OnDeath != null)
                OnDeath(gameObject);
        }

        public void Heal(int amount)
        {
            _health += amount;
            Clamp();
        }

        private void Clamp()
        {
            _health = Mathf.Clamp(_health, 0, Initial);
        }
    }
}
