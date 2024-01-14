using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Fishing___Fish
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject fish;
        [SerializeField] private Vector3 spawnAreaMin;
        [SerializeField] private Vector3 spawnAreaMax;
        public float spawnTimer;
        private float _setSpawnTimer = 5f;
        private GameObject _fishInGame;
        // Start is called before the first frame update
        private void Start()
        {
            spawnTimer = _setSpawnTimer;
            _fishInGame = GameObject.Find("Fish");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_fishInGame != null) return;
            spawnTimer -= Time.deltaTime;
            
            if (!(spawnTimer <= 0)) return;
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
            
            spawnTimer = _setSpawnTimer;
            _fishInGame = Instantiate(fish, randomPosition, Quaternion.identity);
        }
    }
}
