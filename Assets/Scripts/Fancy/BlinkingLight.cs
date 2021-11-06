using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class BlinkingLight : MonoBehaviour
{
    float time=0;
    public Light light;
    float baseIntensity;
    public AnimationCurve curve;
    
    public float animationTime;

    private void Start()
    {
        light = GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    private void Update()
    {
        time = (time+Time.deltaTime)%animationTime;
        light.intensity = baseIntensity * curve.Evaluate(time/animationTime);
        Debug.Log(GetComponent<Light>().intensity);
    }
}
