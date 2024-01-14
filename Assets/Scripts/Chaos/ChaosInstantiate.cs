using System.Collections;
using System.Collections.Generic;
using Fishing___Fish;
using Unity.VisualScripting;
using UnityEngine;

namespace Chaos
{
    public class ChaosInstantiate : MonoBehaviour
    {
        private GameObject player;
    
        [Header("Bear")]
        [SerializeField] private GameObject bear;
        [SerializeField] private List<Transform> bearSpawnerPositions;
    
        public void InstantiateBear()
        { 
            // Spawnaa karhun kerran. Euler on tässä tapauksessa rotaatio
            Instantiate(bear, bearSpawnerPositions[Random.Range(0, bearSpawnerPositions.Count)].position, Quaternion.identity);
        }

        [Header("Seagull")] 
        [SerializeField] private GameObject seagull;
        [SerializeField] private List<Transform> seagullSpawnerPositions;

        public void InstantiateSeagull()
        {
            Instantiate(seagull, seagullSpawnerPositions[Random.Range(0, seagullSpawnerPositions.Count)].position, Quaternion.identity);
        }

        [Header("Flashbang")] 
        [SerializeField] private GameObject flashbang;
        [SerializeField] private Vector3 flashbangSpawnPosition;

        public void InstantiateFlashbang()
        {
            flashbangSpawnPosition = player.transform.position 
                                     + player.transform.forward * 5  
                                     + player.transform.up * 2;
            var spawnedFlash = Instantiate(flashbang, flashbangSpawnPosition, Quaternion.identity);
            var spawnedFlashRb = spawnedFlash.GetComponent<Rigidbody>();
            spawnedFlashRb.AddTorque(spawnedFlash.transform.up * 1000);
        }

        [Header("QR Code")] 
        [SerializeField] private GameObject qrCode;
        [SerializeField] public FishPile fishScript;

        public void InstantiateQRCode()
        {
            StartCoroutine(fishScript.ChangeFish(qrCode));
        }

        [Header("Game world")] 
        [Tooltip("ADD EVERY SINGLE ICE PREFAB IN THE GAME WORLD HERE")]
        [SerializeField] private List<GameObject> worldIce;
        
        public IEnumerator DestroyIceTimer()
        {
            foreach (var ice in worldIce)
                ice.SetActive(false);
            
            yield return new WaitForSeconds(1);
            
            foreach (var ice in worldIce)
                ice.SetActive(true);
        }
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            fishScript = GameObject.Find("FishPile").GetComponent<FishPile>();
        }

        // Update is called once per frame

    }
}
