using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Utils
{
    public class DestroyAfterAndWaitForPS : MonoBehaviour
    {
        public float Time = 1f;
        public ParticleSystem ParticleSystem;

        void Start()
        {
            StartCoroutine(DelayParticleSystem());
        }

        public IEnumerator DelayParticleSystem()
        {
            yield return new WaitForSeconds(Time);

            if (ParticleSystem != null)
                ParticleSystem.Stop();

            while (ParticleSystem != null && ParticleSystem.IsAlive())
                yield return new WaitForEndOfFrame();

            Destroy(gameObject);
        }
    }
}
