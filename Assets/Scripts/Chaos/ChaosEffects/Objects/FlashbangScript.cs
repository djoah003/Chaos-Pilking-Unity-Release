using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chaos.ChaosEffects.Objects
{
    public class FlashbangScript : MonoBehaviour
    {
        [Header("Audio stuff")]
        [SerializeField] private List<AudioClip> flashbangAudioClips;
        /*  0 - Flashbang spawn
     *  1 - Flashbang explode
     *  2 - Tinnitus
     *  3 - Flashbang hits the floor
     */
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private float audioInitialVolume;
        private Coroutine _flashCoroutine;

        [Header("Visual stuff")] [SerializeField]
        private Image flashbangEffect;
        private bool _hasBeenFlashed = false;
        private MeshRenderer flashbangMesh;
        [SerializeField] private float flashDuration = 60f;
        // Start is called before the first frame update
        void Start()
        {
            flashbangMesh = gameObject.GetComponent<MeshRenderer>();
            flashbangEffect = GameObject.Find("Flashbang Effect").GetComponent<Image>();
            playerAudioSource = GameObject.Find("PlayerSoundEffects").GetComponent<AudioSource>();
            playerAudioSource.volume = audioInitialVolume;
        
            flashbangEffect.enabled = false;
            flashbangEffect.CrossFadeAlpha(1, 0, false);
        
            _flashCoroutine = StartCoroutine(FlashBangExplode());
        }

        // Update is called once per frame
        void Update()
        {
            if (!_hasBeenFlashed) return;
            flashbangEffect.CrossFadeAlpha(0, flashDuration, false);
        
        }
        private IEnumerator FlashBangExplode()
        {
            // Play the spawn sound
            audioSource.clip = flashbangAudioClips[0];
            audioSource.Play();
        
            yield return new WaitForSeconds(2); // Wait a few seconds before explosion
        
            // Play the explosion sound
            audioSource.clip = flashbangAudioClips[1];
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
            flashbangMesh.enabled = false;
            flashbangEffect.enabled = true;
        
            yield return new WaitForSeconds(1);
            // Play Tinnitus
            playerAudioSource.volume = audioInitialVolume;
            playerAudioSource.clip = flashbangAudioClips[2];
            playerAudioSource.Play();
        
        
            yield return new WaitForSeconds(5);
            _hasBeenFlashed = true;

            float currentTime = 0;
            while (currentTime < flashDuration)
            {
                currentTime += Time.deltaTime;
                playerAudioSource.volume = Mathf.Lerp(audioInitialVolume, 0, currentTime / flashDuration);
                yield return null;
                Debug.Log(playerAudioSource.volume);
            }
            Destroy(gameObject);
        }
 
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Terrain"))
            {
                // Play hit floor sound
            }
        }
    }
}
