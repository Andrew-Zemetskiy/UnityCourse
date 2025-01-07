using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxLayer : MonoBehaviour
{
    public Transform layerTransform;
    public float parallaxFactor;
    public float layerWidth;

    private void Awake()
    {
        layerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }
}