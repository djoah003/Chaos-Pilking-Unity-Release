using System.Collections;
using UnityEngine;

namespace Chaos.ChaosEffects.Player
{
    public class PlayerEffects : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private GameObject player;
        private Rigidbody _playerRb;
        [SerializeField] private PlayerMovement playerScript;
        // Start is called before the first frame update
        void Start()
        {
            player = gameObject;
            _playerRb = player.GetComponent<Rigidbody>();
            playerScript = player.GetComponent<PlayerMovement>();
        }

        public void PlayerThrow()
        {
            // Move the player relative to the bear's direction.
            _playerRb.AddForce(gameObject.transform.forward * Random.Range(100f, 1000f));
            // Throw the player up.
            _playerRb.AddForce(Vector3.up * Random.Range(100f, 1000f));
        }

        public void PlayerSlip()
        {
            player.transform.Rotate(Vector3.forward, 50.0f);
        }

        public IEnumerator FakeCrash()
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 2f));
            Time.timeScale = 1;
            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 1f));
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(Random.Range(1f, 10f));
            Time.timeScale = 1;
        }
    }
}
