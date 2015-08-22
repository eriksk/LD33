using System;
using System.Collections.Generic;
using Assets._Project.Scripts.SpriteSheets;
using UnityEngine;

namespace Assets._Project.Scripts.Animations
{
    public class SpriteAnimations : MonoBehaviour
    {
        public MeshRenderer Renderer;
        public List<SpriteAnimation> Animations = new List<SpriteAnimation>();

        private SpriteAnimation _currentAnimation;
        private int _oldFrame;

        public delegate void AnimationEnd();
        public event AnimationEnd OnAnimationEnd;

        public void SetAnim(string name)
        {
            if(_currentAnimation != null && _currentAnimation.Name == name)
                return;

            _currentAnimation = Animations.Find(x => x.Name == name); // TODO: don't use linq
            _currentAnimation.Reset();
        }

        public string CurrentAnimation
        {
            get { return _currentAnimation == null ? "None" : _currentAnimation.Name; }
        }

        void Update()
        {
            if (_currentAnimation == null)
                return;

            if (_currentAnimation.Update())
            {
                if (OnAnimationEnd != null)
                    OnAnimationEnd();
            }

            ApplyAnimationToRenderer();
        }

        private void ApplyAnimationToRenderer()
        {
            if (Renderer == null)
                return;

            var frame = _currentAnimation.Frame;
            if (frame == _oldFrame)
                return;

            _oldFrame = frame;

            // TODO: cache some stuff
            var material = Renderer.material;
            var sheet = new SpriteSheet(material, 32);
            var cell = sheet.GetCellFromIndex(frame);
            var uv = sheet.GetUvCoordsForCell(cell);
            material.SetTextureOffset("_MainTex", uv);
            material.SetTextureOffset("_BumpMap", uv);
        }
    }

    [Serializable]
    public class SpriteAnimation
    {
        public string Name;
        public int[] Frames = new int[0];
        public float Interval = 100f;

        private float _current;
        private int _currentFrame;

        public int Frame
        {
            get { return Frames[_currentFrame]; }
        }

        public void Reset()
        {
            _current = 0;
            _currentFrame = 0;
        }

        public bool Update()
        {
            _current += Time.deltaTime*1000f;
            if (!(_current >= Interval)) return false;

            _current = 0f;
            _currentFrame++;

            if (_currentFrame <= Frames.Length - 1) return false;

            Reset();
            return true;
        }

        public override string ToString()
        {
            return Name + "what";
        }
    }
}
