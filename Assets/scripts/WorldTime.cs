using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class WorldTime : MonoBehaviour
{
    public Light gameLight;
    public TextMeshProUGUI gameClock;
    public Gradient lightGradient;
    public float inGameDayLength; //seconds
    private TimeSpan currentTime;

    public Gradient skyTint;
    private Material skyboxMaterial;

    private float oneMinuteLength => inGameDayLength / 1440;   // gameDay / irlDay
    public bool isNight = false;

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        currentTime = new TimeSpan(7, 0, 0);
        StartCoroutine(MinutePassed());
    }

    private void Update()
    {
        gameLight.color = lightGradient.Evaluate(PercentOFDay(currentTime));
        skyboxMaterial.SetColor("_SkyTint", skyTint.Evaluate(PercentOFDay(currentTime)));
        skyboxMaterial.SetColor("_GroundColor", skyTint.Evaluate(PercentOFDay(currentTime)));

    }

    private IEnumerator MinutePassed()
    {
        currentTime += TimeSpan.FromMinutes(1);             // add one minute
        gameClock.SetText(currentTime.ToString(@"hh\:mm"));
        CheckIfNight();
        yield return new WaitForSeconds(oneMinuteLength);   // wait for one minute in game
        StartCoroutine(MinutePassed());                     //infinite loop
    }
 

    private float PercentOFDay(TimeSpan timespan)
    {
        return (float)timespan.TotalMinutes % 1440 / 1440;   //when 1.15 make 0.15
    }

    private void CheckIfNight()
    {
        int hour = currentTime.Hours;
        if(hour <= 5 || hour >= 22)
        {
            isNight = true;
        }
        else
        {
            isNight= false;
        }
    }

    public bool IsNight { get { return isNight; } }
}
