using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float movementSpeed = 5.0f; [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float timer;
    [FormerlySerializedAs("_hasFallen")] public bool hasFallen;
    [FormerlySerializedAs("_isGettingUp")] public bool isGettingUp;
    private Coroutine _coroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        mainCamera = Camera.main.transform;
    }

    private IEnumerator FallTimer()
    {
        hasFallen = true;
        
        // Debug.Log("Coroutine has started");
        yield return new WaitForSeconds(5);
        // When timer reaches 5 seconds do everything under
        // Debug.Log("Coroutine has stopped");

        isGettingUp = true;
        hasFallen = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -10.0f) // OUT OF BOUNDS FIX
        {
            transform.position = new Vector3(0, 1, 0);
        }
        
        //Debug.Log(Vector3.Angle(Vector3.up, gameObject.transform.up));
        
        // Player rotation
        float targetYRotation = mainCamera.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(playerRb.rotation.eulerAngles.x, targetYRotation, playerRb.rotation.eulerAngles.z);
        
        if(isGettingUp)
        {
            if (Vector3.Angle(Vector3.up, gameObject.transform.up) > 5f)
                playerRb.rotation = Quaternion.Lerp(playerRb.rotation, Quaternion.Euler(0, targetYRotation, 0), 10 * Time.deltaTime);
            else
                isGettingUp = false;
        }
        else
        {
            //Player movement
            if (Vector3.Angle(Vector3.up, gameObject.transform.up) > 10f) // Is falling (Help me god)
            {
                if (playerRb.velocity.y < 0.33f && !isGettingUp && (horizontalInput == 0 || verticalInput == 0))
                {
                    if (!hasFallen)
                       _coroutine = StartCoroutine(FallTimer());
                }
                else if (isGettingUp)
                    playerRb.rotation = Quaternion.Lerp(playerRb.rotation, Quaternion.Euler(0, targetYRotation, 0), 10 * Time.deltaTime);
            }
            else
            {
                if (hasFallen)
                {
                    // Debug.Log("- - - STOP COROUTINE - - -");
                    StopCoroutine(_coroutine);
                    hasFallen = false;
                }
                
                playerRb.rotation = Quaternion.Lerp(playerRb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
                playerRb.transform.Translate(movement * (movementSpeed * Time.deltaTime), Space.Self);
            }
        }
    }
}
