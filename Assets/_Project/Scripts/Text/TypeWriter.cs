using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Project.Scripts.Text
{
    public class TypeWriter
    {
        private readonly string _text;
        private readonly float _interval;
        private readonly bool _useWords;
        private float _current;
        private int _length;

        public TypeWriter([NotNull] string text, float interval, bool useWords = false)
        {
            if (text == null) throw new ArgumentNullException("text");
            _text = text;
            _interval = interval;
            _useWords = useWords;
            _length = 0;
            CurrentText = "";
        }

        public string CurrentText { get; private set; }

        public bool Done
        {
            get { return _length == _text.Length; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if text changed</returns>
        public bool Update()
        {
            if(Done)
                return false;

            _current += Time.deltaTime * 1000f;
            if (_current > _interval)
            {
                _current = 0f;
                NextIndex();
                return true;
            }
            return false;
        }

        private void NextIndex()
        {
            if (_useWords)
            {
                while (_length < _text.Length)
                {
                    if (_text[_length] == ' ')
                    {
                        _length++;
                        break;
                    }

                    _length++;

                    if (_length > _text.Length)
                    {
                        _length = _text.Length;
                        break;
                    }
                }
            }
            else
            {
                _length++;
                if (_length > _text.Length)
                    _length = _text.Length;
            }

            CurrentText = _length == 0 ? "" : _text.Substring(0, _length);
        }

        public void SkipToEnd()
        {
            _current = _text.Length;
        }
    }
}
