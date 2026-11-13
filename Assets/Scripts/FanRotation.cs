using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public Vector3 rotationdirection;
    public float rotation_speed = 45f;

    void Update()
    {
        transform.Rotate(rotationdirection * rotation_speed * Time.deltaTime);
    }
}
