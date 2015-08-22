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

        public delegate void FrameEnter(int frameIndex, int frameValue);
        public event FrameEnter OnFrameEnter;

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

            var frameUpdateResult = _currentAnimation.Update();
            if (frameUpdateResult.AnimationEnded)
            {
                if (OnAnimationEnd != null)
                    OnAnimationEnd();
            }
            if (frameUpdateResult.EnteredNewFrame)
            {
                if (OnFrameEnter != null)
                {
                    OnFrameEnter(_currentAnimation.FrameIndex, _currentAnimation.Frame);
                }
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

        private FrameUpdateResult _result = new FrameUpdateResult();

        public int Frame
        {
            get { return Frames[_currentFrame]; }
        }

        public int FrameIndex
        {
            get { return _currentFrame; }
        }

        public void Reset()
        {
            _current = 0;
            _currentFrame = 0;
        }

        public FrameUpdateResult Update()
        {
            _result.EnteredNewFrame = false;
            _result.AnimationEnded = false;

            _current += Time.deltaTime*1000f;
            if (!(_current >= Interval))
                return _result;

            _current = 0f;
            _currentFrame++;
            _result.EnteredNewFrame = true;

            if (_currentFrame <= Frames.Length - 1)
                return _result;

            _result.AnimationEnded = true;

            Reset();
            return _result;
        }

        public override string ToString()
        {
            return Name + "what";
        }
    }

    public class FrameUpdateResult
    {
        public bool AnimationEnded;
        public bool EnteredNewFrame;
    }
}
