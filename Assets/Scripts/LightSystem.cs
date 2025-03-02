using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] Light light1;
    [SerializeField] Light light2;

    [SerializeField] Color lightColor;
    [SerializeField] float startIntensity;

    public float duration = 2f; // How long the fade should take
    public float targetIntensity = 0f; // The final intensity

    private void Start()
    {
        lightColor = light1.color;
        startIntensity = light1.intensity;
    }

    public void DimLights()
    {
        light1.DOIntensity(targetIntensity, duration);
        light2.DOIntensity(targetIntensity, duration);

        light1.DOColor(Color.blue, duration); // Changes light color over time
        light2.DOColor(Color.blue, duration); // Changes light color over time
    }

    public void UndimLights()
    {
        light1.DOIntensity(startIntensity, duration);
        light2.DOIntensity(startIntensity, duration);

        light1.DOColor(lightColor, duration); // Changes light color over time
        light2.DOColor(lightColor, duration); // Changes light color over time
    }
}
