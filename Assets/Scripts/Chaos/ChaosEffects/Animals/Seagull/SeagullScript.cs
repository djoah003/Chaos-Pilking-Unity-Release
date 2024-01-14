using UnityEngine;

namespace Chaos.ChaosEffects.Animals.Seagull
{
    public class SeagullScript : ChaosAnimals
    {

        [SerializeField] private GameObject player;
        [SerializeField] private bool isSeen;
        [SerializeField] private Camera mainCamera;

        private Rigidbody rb;
    

        public bool playerSees;
        // Start is called before the first frame update
        private new void Start()
        {
            base.Start();
            player = GameObject.Find("Player");
            mainCamera = Camera.main;
            rb = gameObject.GetComponent<Rigidbody>();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FishPile"))
                FishPileHit();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position); //get viewport positions
            if ((screenPos.x is >= -0.33f and <= 1.33f) && (screenPos.y is >= -0.33f and <= 1.33f && screenPos.z > 0))
            {
                // On screen
                isSeen = true;
            }
            else
            {
                // Not on screen
                isSeen = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.LookAt(player.transform);
                // transform.position = Vector3.Lerp(transform.position, fishPileObject.transform.position, Time.deltaTime * .33f);
                rb.position = Vector3.MoveTowards(transform.position, fishPileObject.transform.position, Time.deltaTime * 2f);
            }
        }
    }
}
