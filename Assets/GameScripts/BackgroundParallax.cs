using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

// @NOTE the attached sprite's position should be "top left" or the children will not align properly
// Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
[RequireComponent(typeof(SpriteRenderer))]

// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
public class BackgroundParallax : MonoBehaviour
{
    SpriteRenderer sprite;

    private float depth = 1;
    private float v;
    private float h;

    void Awake()
    {

        // Get the current sprite with an unscaled size
        sprite = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
        transform.position += new Vector3(spriteSize.x * 2, 0);

        // Generate a child prefab of the sprite renderer
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;
        childPrefab.transform.localScale = transform.localScale;

        // Loop through and spit out repeated tiles
        GameObject child;
        for (int i = 1; i < 5; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            child.transform.position = transform.position - (new Vector3(spriteSize.x, 0, 0) * i);
            child.transform.parent = transform;
        }

        // Set the parent last on the prefab to prevent transform displacement
        childPrefab.transform.parent = transform;

        // Disable the currently existing sprite component since its now a repeated image
        sprite.enabled = false;
    }

    void Start()
    {
        depth = transform.localPosition.z;        
    }

    void Update()
    {
        v = CrossPlatformInputManager.GetAxis("Vertical");
        h = CrossPlatformInputManager.GetAxis("Horizontal");
        transform.position += new Vector3((7 - depth) * Time.fixedDeltaTime * -h * 0.1f, 0);
    }
}