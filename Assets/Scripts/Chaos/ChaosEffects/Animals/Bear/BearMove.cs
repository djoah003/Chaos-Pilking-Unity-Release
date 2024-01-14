using System;
using System.Collections.Generic;
using Fishing___Fish;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Chaos.ChaosEffects.Bear
{
    public class BearMove : ChaosAnimals
    {
        public float speed;
        [SerializeField] private GameObject hitObject;
        [SerializeField] private Rigidbody playerRb;
        private Vector3 _hitObjectPosition;
        [SerializeField] private AudioClip playerReaction;
        [SerializeField] private AudioSource playerAudioSource;
 


        private new void Start()
        {
            base.Start();
            hitObject = GameObject.Find("Player");
            playerRb = hitObject.GetComponent<Rigidbody>();

            AnimalAudioSource = gameObject.GetComponent<AudioSource>();
            playerAudioSource = GameObject.Find("PlayerSoundEffects").GetComponent<AudioSource>();
            int audioRnd = Random.Range(0, 2);
            switch (audioRnd)
            {
                case 0:
                    AnimalAudioSource.clip = animalAudio[0]; // Normal audio
                    break;
                case 1:
                    AnimalAudioSource.clip = animalAudio[1]; // Hur hur hur
                    playerAudioSource.clip = playerReaction;
                    playerAudioSource.Play();
                    break;
            }
            AnimalAudioSource.Play();
        }

        void PlayerHit()
        {
            Debug.Log("Hit");
            // Move the player relative to the bear's direction.
            playerRb.AddForce(gameObject.transform.forward * Random.Range(100f, 1000f));
            // Throw the player up.
            playerRb.AddForce(Vector3.up * Random.Range(100f, 1000f));

            FishCount = CheckFishAmount();

            if (FishCount > 0)
            {
                hitObject = fishPileObject;
                FishCount = 0;
            }
            else
                Destroy(gameObject);
        }
        

        void Update()
        {
            _hitObjectPosition = hitObject.transform.position;
            
            // Karhu liikkuu eteenp?in
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
            gameObject.transform.LookAt(_hitObjectPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                PlayerHit();
            if (other.gameObject.CompareTag("FishPile"))
                FishPileHit();

        }
        
    }
}
