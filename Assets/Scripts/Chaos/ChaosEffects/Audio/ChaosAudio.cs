using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaos.ChaosEffects.Audio
{
    public class ChaosAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> ChaosAudios;
        /* 0 - Discord ping
     * 1 - Knocking sound
     * 2 - Knocking sound 2
     * 3 - Jumpscare
     */
        private Coroutine _audioCoroutine;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator DiscordPing()
        {
            yield return new WaitForSeconds(Random.Range(1, 30));
            audioSource.clip = ChaosAudios[0];
            audioSource.Play();
            yield return new WaitForSeconds(Random.Range(1, 30));
            audioSource.clip = ChaosAudios[0];
            audioSource.Play();
        }
    
        public IEnumerator KnockingSound()
        {
            var rng = Random.Range(0, 2);
            switch (rng)
            {
                case 0:
                    yield return new WaitForSeconds(Random.Range(1, 30));
                    audioSource.clip = ChaosAudios[1];
                    audioSource.Play();
                    break;
                case 1 :
                    yield return new WaitForSeconds(Random.Range(1, 30));
                    audioSource.clip = ChaosAudios[2];
                    audioSource.Play();
                    break;
            }

        }

        public IEnumerator Jumpscare()
        {
            yield return new WaitForSeconds(1);
        
        }
    }
}
