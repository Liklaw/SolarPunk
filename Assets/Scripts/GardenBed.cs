using UnityEngine;

public class GardenBed : MonoBehaviour
{
    // Position where the seed is planted
    public Transform seedPos;

    // Position where the vegetable appears
    public Transform vegetablePos;

    // Indicates whether the garden bed is occupied
    public bool isOccupied = false;

    // Particle effect played when a seed is planted
    public ParticleSystem groundedVFX;

    // Sound played when a seed is planted
    public AudioClip seedDrop;
}