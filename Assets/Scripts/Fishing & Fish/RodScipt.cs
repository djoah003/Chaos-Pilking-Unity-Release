using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodScipt : MonoBehaviour
{
    public Animator animator;
    public AudioClip[] baitingSounds;
    [SerializeField] private AudioSource AudioSource;
    bool baiting = false;
    bool soundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        //animator.Baiting = true;
        //animator.SetBool("Baiting", true);    
    }

    // Update is called once per frame
    public void Baiting() {
        animator.SetBool("Baiting", true);
        Playclip();
        baiting = true;
    }
    public void Catch() {
        animator.SetBool("Catch", true);
        baiting = false;
        soundPlayed = false;
    }
    public void Falsify()
    {
        animator.SetBool("Catch", false);
        animator.SetBool("Baiting", false);
    }
    private void Playclip()
    {
        if (soundPlayed== false) {
            AudioSource.clip = baitingSounds[Random.Range(0, baitingSounds.Length)];
            AudioSource.Play();
            soundPlayed = true;
        }
        
        
    }
    void Update()
    {
        
    }
}
