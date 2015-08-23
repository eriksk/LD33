using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CamShake : MonoBehaviour
    {
        private bool _shaking = false;
        private float _duration;
        private float _current;
        private float _mag;

        public float Multiplier = 1f;

        public void Shake(float duration, float mag)
        {
            _duration = duration;
            _current = 0f;
            _mag = mag* Multiplier;
            _shaking = true;
        }

        void Update()
        {
            if (!_shaking) return;

            _current += Time.deltaTime*1000f;
            if (_current > _duration)
            {
                _shaking = false;
                return;
            }

            float progress = 1f - (_current/_duration);
            float force = _mag*progress;

            float angle = Random.Range(0f, Mathf.PI*2f);

            var offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            transform.position = transform.position + new Vector3(offset.x, offset.y, 0f)*force;
        }
    }
}
