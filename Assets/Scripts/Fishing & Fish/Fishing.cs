using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fishing___Fish
{
    public class Fishing : MonoBehaviour
    {
        [SerializeField] private GameObject fishingLine;
        [SerializeField] private float fishingTimer, setFishingTimer;
        [SerializeField] private bool isFishInLine = false;
        [SerializeField] private Slider fishSlider;
        [SerializeField] private bool doFishEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;
        public BasicFishAI _fishScript;
        private FishPile _fishPile;
        public int fishPileCounter = 0;
        private GameObject _fishObject; // FISH OBJECT :DDDD
        private RodScipt _rodScript;

        private void Start()
        {
            _fishScript = GameObject.Find("Fish").GetComponent<BasicFishAI>(); // Get the script in the ONLY Fish.
            _fishPile = GameObject.Find("FishPile").GetComponent<FishPile>();
            fishSlider = GameObject.Find("FishTimer").GetComponent<Slider>();
            _rodScript = GameObject.Find("Vapa").GetComponent<RodScipt>();
            //fishingTimer = setFishingTimer;         // Set the fishing timer with the setter
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("Something in collider");
            if (other.gameObject.CompareTag("Fish"))
            {
                //Debug.Log("Fish in collider");
                isFishInLine = true;
                _fishObject = other.gameObject; // Set the fish on the hook as the object.
                doFishEffect = true;
            }
            // else if (other.gameObject.CompareTag("Fishing Hole")) // Check if player is on the fishing hole fishing.
            // {
            //     _fishScript.playerFishing = true;
            // }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Fishing Hole"))
            {
                _fishScript.playerFishing = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Fishing Hole"))
            {
                _fishScript.playerFishing = true;
            }
        }

        private void PullItemFromWater()
        {
            //Debug.Log("Item is pulled from the water");
            Destroy(_fishObject);
            _rodScript.Catch();

            _audioSource.clip = _audioClip;
            _audioSource.Play();

            // Check fishpile amount to not get errors.
            if (fishPileCounter <= _fishPile.fishes.Length)
            {
                _fishPile.fishes[fishPileCounter].SetActive(true);
                fishPileCounter++;
            }
            
            isFishInLine = false;
        }

        private void FishPull(float fishPullStrength) // Slider movement to alert the player
        {
            while (fishSlider.value < fishPullStrength)
            {
                fishSlider.value += Time.deltaTime;
            }
        }

        private void Update()
        {
            switch (isFishInLine && _fishScript.playerFishing)
            { 
                case true:
                    if (doFishEffect) // Above void
                    {
                        FishPull(5.0f);
                        doFishEffect = false;
                    }
                    if (fishSlider.value > 0)
                    {
                        fishSlider.value -= 6 * Time.deltaTime;
                        fishingTimer -= Time.deltaTime;
                        _rodScript.Baiting();
                        
                        if (fishingTimer <= 0)
                        {
                            PullItemFromWater();
                        }
                        else if (Input.GetMouseButtonDown(0))
                        {
                            fishSlider.value += 1;
                        }
                    }
                    
                    else
                    {
                        isFishInLine = false;
                    }
                    break;
                
                case false:
                    fishingTimer = setFishingTimer;
                    while (fishSlider.value > 0)
                    {
                        fishSlider.value -= Time.deltaTime;
                    }
                    break;
            }
        }
    
    }
}
