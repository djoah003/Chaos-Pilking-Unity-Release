using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    private Vector3 ogPosition;
    private Vector3 newPosition;
    [SerializeField] private float distance;
    [SerializeField] private bool isTouchingTerrain = false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        ogPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = transform.position;
        if (!(Vector3.Distance(ogPosition, newPosition) > distance) && isTouchingTerrain) return;

        _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        _audioSource.Play();
        ogPosition = transform.position;
    }
    
    private void OnTriggerStay(Collider other)
    {
        isTouchingTerrain = other.gameObject.CompareTag("Terrain");
    }
    private void OnTriggerExit(Collider other)
    {
        isTouchingTerrain = false;
    }

}
