using UnityEngine;

public class MapLevelManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color32 startColor = new Color32(255, 255, 255, 128); // Default transparent white
    public Color32 hoverColor = new Color32(255, 0, 0, 128);    // Transparent red
    private Color32 oldColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;
    }

    void OnMouseEnter()
    {
        oldColor = spriteRenderer.color;
        spriteRenderer.color = hoverColor; // Highlight region on hover
    }

    void OnMouseExit()
    {
        spriteRenderer.color = oldColor; // Reset to default when mouse exits
    }
}