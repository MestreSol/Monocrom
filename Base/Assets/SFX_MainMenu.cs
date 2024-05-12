using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_MainMenu : MonoBehaviour
{
    public FMODUnity.EventReference ambienceRef;

    public float rainIntensity;
    public float windIntensity;
    public float coverIntensity;

    float originalRainIntensity;

    public ParticleSystem rainParticles;

    public Vector3 minRainPosition;
    public Vector3 maxRainPosition;

    FMOD.Studio.EventInstance ambience;

    FMOD.Studio.PARAMETER_ID windParamID;
    FMOD.Studio.PARAMETER_ID rainParamID;
    FMOD.Studio.PARAMETER_ID coverParamID;

    public float rain { get { return rainIntensity; } }

    private void Start()
    {
        ambience = FMODUnity.RuntimeManager.CreateInstance(ambienceRef);
        ambience.start();

        FMOD.Studio.EventDescription eventDescription;
        ambience.getDescription(out eventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION windParamDesc, rainParamDesc, coverParamDesc;

        eventDescription.getParameterDescriptionByName("Wind", out windParamDesc);
        eventDescription.getParameterDescriptionByName("Rain", out rainParamDesc);
        eventDescription.getParameterDescriptionByName("Cover", out coverParamDesc);

        windParamID = windParamDesc.id;
        rainParamID = rainParamDesc.id;
        coverParamID = coverParamDesc.id;

        rainIntensity = Mathf.Round(Random.Range(0, 10f)) / 10f;
        windIntensity = rainIntensity/2;
        coverIntensity = Mathf.Round(Random.Range(0, 10f)) / 10f;

        originalRainIntensity = rainParticles.emission.rateOverTime.constantMax;

        minRainPosition = rainParticles.transform.position;

    }
    private void Update()
    {
        ambience.setParameterByID(windParamID, windIntensity);
        ambience.setParameterByID(rainParamID, rainIntensity);
        ambience.setParameterByID(coverParamID, coverIntensity);


        var emission = rainParticles.emission;
        emission.rateOverTime = Mathf.Lerp(0, originalRainIntensity, rainIntensity);

        var force = rainParticles.forceOverLifetime;
        force.x = new ParticleSystem.MinMaxCurve(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(250f, 0, 0), windIntensity).x);
    }

    private void OnDestroy()
    {
        ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambience.release();
    }
}
