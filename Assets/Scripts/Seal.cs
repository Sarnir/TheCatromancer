using UnityEngine;
using System.Collections;

public class Seal : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer incompleteSeal = null;

    [SerializeField]
    Sprite sealMask = null;

    [SerializeField]
    [Range(1, 1000)]
    int PaintRadius = 1;

    Texture2D sealTexture;

    Color[] maskPixels;
    Color[] sealPixels;

    void Start()
    {
        sealTexture = incompleteSeal.sprite.texture;
        maskPixels = sealMask.texture.GetPixels();
        sealPixels = new Color[sealMask.texture.height * sealMask.texture.width];
        sealTexture.SetPixels(sealPixels);
        sealTexture.Apply();
        incompleteSeal.enabled = true;
    }

    void UpdateSealSprite(Vector2 positionPercent)
    {
        Vector2 point = new Vector2(positionPercent.x * sealTexture.width, positionPercent.y * sealTexture.height);
        int pixelIndex = (int)(sealTexture.width * point.y + point.x);

        for (int y = (int)point.y - PaintRadius; y < (int)point.y + PaintRadius; y++)
        {
            for (int x = (int)point.x - PaintRadius; x < (int)point.x + PaintRadius; x++)
            {
                if (x >= 0 && x < sealTexture.width && y >= 0 && y < sealTexture.height)
                {
                    var distance = Vector2.Distance(point, new Vector2(x, y));
                    if (distance < PaintRadius)
                    {
                        pixelIndex = (int)(sealTexture.width * y + x);

                        if (maskPixels[pixelIndex].a == 0f)
                            sealPixels[pixelIndex] = new Color(0f, 1f, 1f, 1f);
                    }
                }
            }
        }
        
        sealTexture.SetPixels(sealPixels);
        sealTexture.Apply();
    }

    public void PaintSealAtPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        Vector2 percentage = new Vector2();

        if (hit.collider != null)
        {
            var other = hit.collider.gameObject;
            var imageSize = other.GetComponent<SpriteRenderer>().bounds.size;

            var bottomLeftPoint = (other.transform.position - imageSize * 0.5f);
            var topRightPoint = (other.transform.position + imageSize * 0.5f);

            percentage.x = (position.x - bottomLeftPoint.x) / (topRightPoint.x - bottomLeftPoint.x);
            percentage.y = (position.y - bottomLeftPoint.y) / (topRightPoint.y - bottomLeftPoint.y);

            Debug.Log("Mouse Position: " + position);
            Debug.Log("Image size: "+ imageSize.x + " " + imageSize.y);
            Debug.Log("Percentage of the image: " + percentage.x + "% " + percentage.y + "%");

            UpdateSealSprite(percentage);
        }
    }
}
