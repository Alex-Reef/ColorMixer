using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer
{
    public static int ColorEquals(Color color1, Color color2)
    {
        float percentRed;
        if (color1.r < color2.r)
            percentRed = color1.r / color2.r * 100;
        else
            percentRed = color2.r / color1.r * 100;

        float percentGreen;
        if (color1.g < color2.g)
            percentGreen = color1.g / color2.g * 100;
        else
            percentGreen = color2.g / color1.g * 100;

        float percentBlue;
        if (color1.b < color2.b)
            percentBlue = color1.b / color2.b * 100;
        else
            percentBlue = color2.b / color1.b * 100;

        int percent = Mathf.RoundToInt((percentRed + percentGreen + percentBlue) / 3);
        if (percent > 100)
            percent = 100;

        return percent;
    }

    public static Color ColorMix(Color currentColor, GameObject[] Objects)
    {
        float totalRed = currentColor.r;
        float totalGreen = currentColor.g;
        float totalBlue = currentColor.b;

        foreach (Color colour in ObjectToColor(Objects))
        {
            totalRed += colour.r;
            totalGreen += colour.g;
            totalBlue += colour.b;
        }

        float numColours = Objects.Length;
        return new Color(totalRed / numColours, totalGreen / numColours, totalBlue / numColours);
    }

    private static Color[] ObjectToColor(GameObject[] Objects)
    {
        List<Color> colours = new List<Color>();
        foreach (var obj in Objects)
        {
            colours.Add(obj.GetComponent<Fruit>().MainMaterial.color);
            obj.SetActive(false);
        }

        return colours.ToArray();
    }
}
