namespace Assets._Project.Scripts.Utils
{
    public class TimerTrig
    {
        private float _interval;
        private float _current;

        public TimerTrig(float interval)
        {
            _interval = interval;
        }

        public void Set(float interval)
        {
            _interval = interval;
            _current = 0f;
        }

        public bool IsTrigged(float delta)
        {
            _current += delta;
            if (_current >= _interval)
            {
                _current = 0f;
                return true;
            }
            return false;
        }
    }
}
