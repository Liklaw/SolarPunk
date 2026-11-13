using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    // Reference to the drone
    private DroneInteractions drone;

    private void Awake()
    {
        // Gets the drone component from the parent object
        drone = GetComponentInParent<DroneInteractions>();
    }

    // Detects seeds and vegetables inside the trigger
    private void OnTriggerStay(Collider other)
    {
        // Checks if the object is a seed
        SeedType seed = other.GetComponentInParent<SeedType>();

        if (seed != null)
        {
            seed.SetWatering(drone.isWatering);
        }

        // Checks if the object is a vegetable
        VegetableGrowth vegetables = other.GetComponentInParent<VegetableGrowth>();

        if (vegetables != null)
        {
            vegetables.SetWatering(drone.isWatering);
            vegetables.Harvest();
        }
    }

    // Stops watering when an object leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        SeedType seed = other.GetComponentInParent<SeedType>();

        if (seed != null) seed.SetWatering(false);

        VegetableGrowth vegetables = other.GetComponentInParent<VegetableGrowth>();

        if (vegetables != null) vegetables.SetWatering(false);
    }
}