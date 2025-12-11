using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private bool isLooping = true;
    
    [Header("Tiling Settings")]
    [SerializeField] private float tileWidth = 20f;
    
    private Material material;
    private Vector2 offset;
    
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = new Material(renderer.sharedMaterial);
            renderer.material = material;
        }
        else
        {
            Debug.LogError("No Renderer component found on " + gameObject.name);
        }
    }
    
    void Update()
    {
        if (material != null && isLooping)
        {
            offset.x += scrollSpeed * Time.deltaTime;
            material.mainTextureOffset = offset;
        }
        else if (!isLooping)
        {
            transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }
    }
    
    void LateUpdate()
    {
        if (!isLooping && transform.position.x < -tileWidth)
        {
            Vector3 pos = transform.position;
            pos.x += tileWidth * 2;
            transform.position = pos;
        }
    }
}