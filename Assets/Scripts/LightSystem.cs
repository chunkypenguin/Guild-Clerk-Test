using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] Light light1;
    [SerializeField] Light light2;
    [SerializeField] Light light3;
    [SerializeField] Light light4;

    [SerializeField] Color lightColor;
    [SerializeField] float startIntensity;

    public float duration = 2f; // How long the fade should take
    public float targetIntensity = 0f; // The final intensity

    public static LightSystem instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        lightColor = light1.color;
        startIntensity = light1.intensity;
    }

    public void DimLights()
    {
        light1.DOIntensity(targetIntensity, duration);
        light2.DOIntensity(targetIntensity, duration);
        light3.DOIntensity(targetIntensity, duration);
        light4.DOIntensity(targetIntensity, duration);

        light1.DOColor(Color.blue, duration); // Changes light color over time
        light2.DOColor(Color.blue, duration); // Changes light color over time
        light3.DOColor(Color.blue, duration); // Changes light color over time
        light4.DOColor(Color.blue, duration);
    }

    public void DimLightsRed()
    {
        light1.DOIntensity(targetIntensity, duration);
        light2.DOIntensity(targetIntensity, duration);
        light3.DOIntensity(targetIntensity, duration);
        light4.DOIntensity(targetIntensity, duration);

        light1.DOColor(Color.magenta, duration); // Changes light color over time
        light2.DOColor(Color.magenta, duration); // Changes light color over time
        light3.DOColor(Color.magenta, duration); // Changes light color over time
        light4.DOColor(Color.magenta, duration);
    }

    public void UndimLights()
    {
        light1.DOIntensity(startIntensity, duration);
        light2.DOIntensity(startIntensity, duration);
        light3.DOIntensity(startIntensity, duration);
        light4.DOIntensity(startIntensity, duration);

        light1.DOColor(lightColor, duration); // Changes light color over time
        light2.DOColor(lightColor, duration); // Changes light color over time
        light3.DOColor(lightColor, duration); // Changes light color over time
        light4.DOColor(lightColor, duration);
    }
}
