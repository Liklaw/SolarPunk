using UnityEngine;
using System.Collections;

public class SeedType : MonoBehaviour
{
    // Indicates if the seed has been planted
    public bool isPlanted = false;

    // Vegetable prefab
    public GameObject vegetablePrefab;

    public float destroyTime = 3f;
    public float growthTime = 2f;
    public float waterGrowthMultiplier = 2f;

    // Drone reference
    public DroneInteractions drone;

    private bool isBeingWatered = false;

    // Score manager reference
    public ScoreManager scoreManager;

    private void Start()
    {
        StartCoroutine(SeedLifetime());
    }

    // Destroys the seed if not planted
    private IEnumerator SeedLifetime()
    {
        yield return new WaitForSeconds(destroyTime);

        if (!isPlanted) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        GardenBed bed = other.GetComponentInParent<GardenBed>();

        if (bed == null || bed.isOccupied || isPlanted)
            return;

        Plant(bed);
    }

    // Plants the seed in the garden bed
    private void Plant(GardenBed bed)
    {
        isPlanted = true;
        bed.isOccupied = true;

        GetComponent<Rigidbody>().isKinematic = true;

        bed.groundedVFX.Play();
        AudioSource.PlayClipAtPoint(bed.seedDrop, transform.position);

        transform.position = bed.seedPos.position;
        transform.rotation = bed.seedPos.rotation;

        StartCoroutine(Grow(bed));
    }

    public void SetWatering(bool value)
    {
        isBeingWatered = value;
    }

    // Grows the seed into a vegetable
    private IEnumerator Grow(GardenBed bed)
    {
        float timer = 0f;

        while (timer < growthTime)
        {
            float speed = 1f;

            if (isBeingWatered)
            {
                speed = waterGrowthMultiplier;
            }

            timer += Time.deltaTime * speed;

            yield return null;
        }

        // Spawns the vegetable
        GameObject vegetables = Instantiate(vegetablePrefab, bed.vegetablePos.position, bed.vegetablePos.rotation);

        VegetableGrowth vegetableGrowth = vegetables.GetComponent<VegetableGrowth>();

        if (vegetableGrowth != null)
        {
            vegetableGrowth.drone = drone;
            vegetableGrowth.scoreManager = scoreManager;
            vegetableGrowth.Initialize(bed);
        }
        else
        {
            bed.isOccupied = false;
        }

        Destroy(gameObject);
    }
}