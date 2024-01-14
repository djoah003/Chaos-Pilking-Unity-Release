using System.Collections;
using System.Collections.Generic;
using Fishing___Fish;
using UnityEngine;

public class ChaosAnimals : MonoBehaviour
{
    [SerializeField] protected GameObject fishPileObject;
    [SerializeField] private Fishing fishingScript;
    protected FishPile FishPile;

    protected int FishCount = 0;
    
    [Header("Audio")] 
    protected AudioSource AnimalAudioSource;
    [SerializeField] protected List<AudioClip> animalAudio;
    
    // Start is called before the first frame update
    protected void Start()
    {
        fishPileObject = GameObject.Find("FishPile");
        FishPile = fishPileObject.GetComponent<FishPile>();
        fishingScript = GameObject.Find("Siima").GetComponent<Fishing>();
    }

    protected int CheckFishAmount()
    {
        // Get the amount of fishes in the fish pile
        foreach (var fish in FishPile.fishes) 
        {
            if (fish.activeSelf)
            {
                FishCount++;
            }
        }

        return FishCount;
    }

    protected void FishPileHit()
    {

        FishCount = CheckFishAmount();
        if (FishCount > 0)
        {
            for (int i = 1; i <= Random.Range(1, FishCount); i++)
            { 
                //Debug.Log("Fish eaten: " + (i));
                // delete a random amount of fishes from the fish pile.
                FishPile.fishes[FishCount - i].SetActive(false);
                fishingScript.fishPileCounter -= i;
            }
        }
        Destroy(gameObject);
    }
    
}
