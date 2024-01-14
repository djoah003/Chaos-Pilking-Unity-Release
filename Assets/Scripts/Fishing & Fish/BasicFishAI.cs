using UnityEngine;

namespace Fishing___Fish
{
    public class BasicFishAI : MonoBehaviour
    {
        
        [SerializeField] public float moveSpeed = 200.0f; // Adjust this speed as needed.

        public GameObject _fishingRod;
        private Transform _fishingRodLocation;
        private Rigidbody _fishRb;
        public bool playerFishing; // Check for fishing. Is true when player is on the fishing hole.
        private bool hasReachedLocation = true;

        private Vector3 attractionDirection;


        private Fishing fishingScript;

        private void Start()
        {
            _fishRb = gameObject.GetComponent<Rigidbody>();
            _fishingRod = GameObject.Find("Hook");
            _fishingRodLocation = _fishingRod.transform;
            fishingScript = GameObject.Find("Siima").GetComponent<Fishing>();
            fishingScript._fishScript = gameObject.GetComponent<BasicFishAI>();

            moveSpeed = Random.Range(100f, 200f);
        }

        private void Update()
        {
            if (_fishingRod == null) return;

            if (playerFishing)
            {
                transform.LookAt(_fishingRodLocation);

                if (transform.position.y > -0.5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), Time.deltaTime);
                }
                else
                {
                    // Calculate the direction from the fish to the fishing rod.
                    Vector3 attractionDirection = (_fishingRodLocation.position - transform.position).normalized;

                    // Move the fish towards the fishing rod gradually using Lerp.
                    Vector3 targetPosition = transform.position + attractionDirection * (moveSpeed * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
                    //_fishRb.velocity = targetPosition * (moveSpeed * Time.deltaTime);
                }
            }
            else // IF PLAYER IS NOT ON THE FISHING HOLE
            {
                    // Calculate the direction from the fish to the new position.
                    if (hasReachedLocation)
                    {
                        attractionDirection = new Vector3(Random.Range(-5, 6), Random.Range(-5, -1), Random.Range(-5, 6));
                        hasReachedLocation = false;
                    }
                    // Move the fish towards the new position gradually using Lerp.
                    //Vector3 targetPosition = attractionDirection * (moveSpeed * Time.deltaTime);
                    //Debug.Log(Vector3.Distance(transform.position, attractionDirection));
                    transform.LookAt(attractionDirection);
                    transform.position = Vector3.MoveTowards(transform.position, attractionDirection, Time.deltaTime);
                    //_fishRb.velocity = targetPosition * (moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, attractionDirection) < 0.5)
                    {
                        hasReachedLocation = true;
                    }
            }
        }
    }
}