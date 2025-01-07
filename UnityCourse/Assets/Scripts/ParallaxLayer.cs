using System;
using UnityEngine;
public class ParallaxLayer : MonoBehaviour
{
    public Transform layerTransform;
    public float parallaxFactor;
    public float layerWidth;

    public Renderer biggestSpriteInLayer;
    public string layerName;

    private void Start()
    {
        layerName = gameObject.name;
        layerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log($"Name {layerName}; Size {layerWidth}");
    }
}