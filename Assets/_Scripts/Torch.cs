using UnityEngine;

public class Torch : MonoBehaviour
{

    [Header("Torch Timers")]
    public float torchDuration;
    public float torchDurationMax;

    [Header("Particle Values")]
    public float rangeMultiplier;
    public float intensityMultiplier;
    public float rateOverTime;
    public float rangeMultiplierMax;
    public float intensityMultiplierMax;
    public float rateOverTimeMax;


    [Header("Particle References")]
    public ParticleSystem lightParticleSystem;
    public ParticleSystem VFXParticleSystem;
    public ParticleSystem VFXParticleSystem2;
    private ParticleSystem.LightsModule lights;
    private ParticleSystem.EmissionModule emmision;
    private ParticleSystem.EmissionModule emmision2;


    private void Start()
    {
        lights = lightParticleSystem.lights;
        emmision = VFXParticleSystem.emission;
        emmision2 = VFXParticleSystem2.emission;
        torchDuration = 0;

        rangeMultiplierMax = lights.rangeMultiplier;
        intensityMultiplierMax = lights.intensityMultiplier;
        rateOverTimeMax = emmision.rateOverTimeMultiplier;
    }

    private void Update()
    {
        var TDbyPercentage = torchDuration / torchDurationMax * 100;
        var rateOverTimePercentageToNormal = TDbyPercentage / 100 * rateOverTimeMax;
        var rangeMultiplierPercentageToNormal = TDbyPercentage / 100 * rangeMultiplierMax;
        var intensityMultiplierPercentageToNormal = TDbyPercentage / 100 * intensityMultiplierMax;
        rateOverTime = rateOverTimePercentageToNormal;
        rangeMultiplier = rangeMultiplierPercentageToNormal;
        intensityMultiplier = intensityMultiplierPercentageToNormal;

        lights.rangeMultiplier = rangeMultiplier;
        lights.intensityMultiplier = intensityMultiplier;
        emmision.rateOverTimeMultiplier = rateOverTime;
        emmision2.rateOverTimeMultiplier = rateOverTime;


        torchDuration -= Time.deltaTime / 2;
    }

    public void RelightTheTorch()
    {
        torchDuration = torchDurationMax;
    }
}
