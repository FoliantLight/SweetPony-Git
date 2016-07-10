using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class deepmap : MonoBehaviour
{
    public List<int> SourcePaletteLevels;
    public List<Color> redPalette;
    public List<Color> bluePalette;
    public List<Color> greenPalette;

    // Use this for initialization
    void Start()
    {
        var t = Instantiate(gameObject.GetComponent<SpriteRenderer>().sprite.texture);
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                var c = t.GetPixel(x, y);

                for (int i = 0; i < SourcePaletteLevels.Count; i++)
                {
                    if (c.a != 0)
                    {
                        float f = SourcePaletteLevels[i] / 255.0f;
                        if (c.b >= f - 0.05f && c.b <= f + 0.05f)
                        {
                            t.SetPixel(x, y, bluePalette[i]);
                            break;
                        }
                        else if (c.g >= f - 0.05f && c.g <= f + 0.05f)
                        {
                            t.SetPixel(x, y, greenPalette[i]);
                            break;
                        }
                    }
                }
            }
        }
        t.Apply(false);
        Sprite s = new Sprite();
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
