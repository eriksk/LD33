using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Characters
{
    public class CharacterFlip : MonoBehaviour
    {
        public Renderer Renderer;
        private bool _flipped;

        public bool Flipped
        {
            get { return _flipped; }
            set
            {
                _flipped = value; 
                if(Flipped)
                    FlipLeft();
                else
                    FlipRight();
            }
        }

        public float FlippedAsUnit
        {
            get { return _flipped ? -1f : 1f; }
        }

        public void FlipLeft()
        {
            _flipped = true;
            if (Renderer.gameObject.transform.localScale.x > 0f)
                Renderer.gameObject.transform.localScale = new Vector3(Renderer.gameObject.transform.localScale.x * -1f, Renderer.gameObject.transform.localScale.y, Renderer.gameObject.transform.localScale.z);
        }

        public void FlipRight()
        {
            _flipped = false;
            if (Renderer.gameObject.transform.localScale.x < 0f)
                Renderer.gameObject.transform.localScale = new Vector3(Renderer.gameObject.transform.localScale.x * -1f, Renderer.gameObject.transform.localScale.y, Renderer.gameObject.transform.localScale.z);
        }
    }
}
