using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Chaos.ChaosEffects.Audio;
using Chaos.ChaosEffects.Bear;
using Chaos.ChaosEffects.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Chaos
{
    public class ChaosTimer : MonoBehaviour
    { 
        [Header("Timer")]
        [SerializeField] private float setChaosTimer = 30.0f;
        [SerializeField] private Slider chaosTimer;

        [Header("Effect targets")]
        [SerializeField] private GameObject player;
        [SerializeField] [Tooltip("The parent of the spawners")] 
        private GameObject spawners;
        [SerializeField] private GameObject qrPile;
        
        [Header("Effects")]
        [SerializeField] private PlayerEffects playerEffects;
        [SerializeField] private ChaosInstantiate chaosInstantiate;
        [SerializeField] private CameraMouse cameraMouse;
        [SerializeField] private ChaosAudio chaosAudio;
        
        private int _switchNumber;
        
        private void Start()
        {
            chaosTimer = GameObject.Find("CHAOStimer").GetComponent<Slider>();
            chaosTimer.maxValue = setChaosTimer;
            
            player = GameObject.Find("Player");
            spawners = GameObject.Find("Spawners");
            
            // INITIALIZE EVERY SCRIPT HERE:

            playerEffects = player.GetComponent<PlayerEffects>();
            cameraMouse = Camera.main.GetComponent<CameraMouse>();
            chaosInstantiate = spawners.GetComponent<ChaosInstantiate>();
            chaosAudio = gameObject.GetComponent<ChaosAudio>();

            // END OF SCRIPT INITIALIZATION

        }

        // Update is called once per frame
        void Update()
        {
            chaosTimer.value += Time.deltaTime;
            if (!(chaosTimer.value >= chaosTimer.maxValue)) return; // EVERYTHING UNDER IS INSIDE IF STATEMENT
            // Assign the _switchNumber to a random digit among the size of the chaosEffects array.
            _switchNumber = Random.Range(0, 12);
            Debug.Log("Chaos effect: " + _switchNumber);
            switch (_switchNumber)
            {
                case 0: // BEAR
                {
                    chaosInstantiate.InstantiateBear();
                    break;
                }
                case 1: // SEAGULL
                {
                    chaosInstantiate.InstantiateSeagull();
                    break;
                }
                case 2: // REVERSE
                { 
                    StartCoroutine(EffectsWithTimer(_switchNumber, setChaosTimer)); // CALL WHEN EFFECT HAS TIMER
                    cameraMouse.reverse = true;
                    break;
                }
                case 3: // MIRROR
                {
                    StartCoroutine(EffectsWithTimer(_switchNumber, setChaosTimer));
                    cameraMouse.xOnY = true;
                    break;
                }
                case 4: // PLAYER THROW
                {
                    playerEffects.PlayerThrow();
                    break;
                }
                case 5: // PLAYER SLIP
                {
                  playerEffects.PlayerSlip();
                  break;
                }
                case 6: // FLASHBANG
                {
                    chaosInstantiate.InstantiateFlashbang();
                    break;
                }
                case 7: // DISCORD PING
                {
                   StartCoroutine(chaosAudio.DiscordPing());
                    break;
                }
                case 8: // KNOCKING SOUND
                {
                    StartCoroutine(chaosAudio.KnockingSound());
                    break;
                }
                case 9: // QR CODE
                {
                    chaosInstantiate.InstantiateQRCode();
                    break;
                }
                case 10: // ICE BREAK
                {
                    StartCoroutine(chaosInstantiate.DestroyIceTimer());
                    break;
                }
                case 11:
                {
                    StartCoroutine(playerEffects.FakeCrash());
                    break;
                }
            }
            chaosTimer.value = 0; // Reset the timer
        }

        private IEnumerator EffectsWithTimer(int index, float timer)
        {
            yield return new WaitForSeconds(timer);
            switch (index)
            {
                case 2: // REVERSE
                { 
                    cameraMouse.reverse = false; // Copy the effect above to false
                    break;
                }
                case 3: // MIRROR
                {
                    cameraMouse.xOnY = false;
                    break;
                }
            }
        }
        
    }
}
