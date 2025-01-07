using System;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;

    public float parallaxEfect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEfect));
        float dist = (cam.transform.position.x * parallaxEfect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}