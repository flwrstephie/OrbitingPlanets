using UnityEngine;
using UnityEngine.UI;

public class StaticFlicker : MonoBehaviour
{
    public Image staticImage;
    public float minAlpha = 0.2f;
    public float maxAlpha = 0.6f;
    public float flickerSpeed = 10f;

    private void Update()
    {
        if (staticImage != null)
        {
            float flicker = Mathf.PerlinNoise(Time.time * flickerSpeed, 0) * (maxAlpha - minAlpha) + minAlpha;
            Color newColor = staticImage.color;
            newColor.a = flicker;
            staticImage.color = newColor;
        }
    }
}
