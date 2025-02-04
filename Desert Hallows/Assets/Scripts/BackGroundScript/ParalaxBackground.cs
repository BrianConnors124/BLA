using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    public float length, startPos;

    public GameObject camera;

    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = camera.transform.position.x * (1 - parallaxEffect);
        var distance = camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
        
    }
}
