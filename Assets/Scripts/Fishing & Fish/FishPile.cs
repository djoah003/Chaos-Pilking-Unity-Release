using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fishing___Fish
{
    public class FishPile : MonoBehaviour
    {
        public GameObject[] fishes;

        public IEnumerator ChangeFish(GameObject GO)
        {
            int fishAmount = fishes.Count(t => t.activeSelf);
            if (fishAmount == 0) yield break;
            int rng = Random.Range(1, fishAmount);

            Mesh resetMesh = fishes[rng].gameObject.GetComponentInChildren<MeshFilter>().mesh;
            Material resetMaterial = fishes[rng].gameObject.GetComponentInChildren<MeshRenderer>().material;
            
            fishes[rng].gameObject.GetComponentInChildren<MeshFilter>().mesh = GO.GetComponent<MeshFilter>().sharedMesh;
            fishes[rng].gameObject.GetComponentInChildren<MeshRenderer>().material = GO.GetComponent<MeshRenderer>().sharedMaterial;
            
            yield return new WaitForSeconds(30f);

            fishes[rng].gameObject.GetComponentInChildren<MeshFilter>().mesh = resetMesh;
            fishes[rng].gameObject.GetComponentInChildren<MeshRenderer>().material = resetMaterial;
        }
        // Start is called before the first frame update
        private void Start()
        {
            int fishPileCount = transform.childCount;

            fishes = new GameObject[fishPileCount];
            for (int i = 0; i < fishPileCount; i++)
            {
                fishes[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
