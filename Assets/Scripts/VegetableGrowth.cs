using UnityEngine;
using System.Collections;

public class VegetableGrowth : MonoBehaviour
{
    // Indicates whether the vegetable has finished growing
    public bool isGrown = false;

    private Animator animator;

    // Time before the vegetable spoils
    public float spoilTime = 5f;

    public float waterGrowthMultiplier = 2f;

    // Material applied when the vegetable spoils
    public Material spoiledMaterial;

    // Harvest particle effect
    public ParticleSystem VegBooms;

    // Drone reference
    public DroneInteractions drone;

    // Gardenbed reference
    private GardenBed gardenBed;
    private bool isHarvested = false;
    private bool isBeingWatered = false;
    private float normalAnimationSpeed = 1f;

    // Points awarded for harvesting
    public int gainPoints = 0;

    public AudioClip explosionSFX;

    [HideInInspector]
    // Score manager reference
    public ScoreManager scoreManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Initializes the vegetable growth
    public void Initialize(GardenBed bed)
    {
        gardenBed = bed;

        animator.speed = normalAnimationSpeed;

        StartCoroutine(Grow());
    }

    // Updates watering state and animation speed
    public void SetWatering(bool value)
    {
        isBeingWatered = value;

        if (animator == null || isGrown) return;

        if (isBeingWatered)
            animator.speed = normalAnimationSpeed * waterGrowthMultiplier;
        else
            animator.speed = normalAnimationSpeed;
    }

    // Handles vegetable growth and spoiling
    private IEnumerator Grow()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        isGrown = true;
        animator.speed = normalAnimationSpeed;

        yield return new WaitForSeconds(spoilTime);

        if (isHarvested) yield break;

        if (spoiledMaterial != null)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.materials;

                for (int i = 0; i < materials.Length; i++)
                    materials[i] = spoiledMaterial;

                renderer.materials = materials;
            }

            gainPoints = 0;
        }
    }

    // Handles harvesting, scoring and effects
    public void Harvest()
    {
        if (!isGrown || isHarvested) return;

        isHarvested = true;

        scoreManager.AddScore(gainPoints);
        gardenBed.isOccupied = false;

        VegBooms.transform.parent = null;

        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        VegBooms.Play();

        Destroy(gameObject);
    }
}