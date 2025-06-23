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

    private float oneMinuteLength => inGameDayLength / 1440;   // gameDay / irlDay

    void Start()
    {
        StartCoroutine(MinutePassed());
    }

    private void Update()
    {
        gameLight.color = lightGradient.Evaluate(PercentOFDay(currentTime));
    }

    private IEnumerator MinutePassed()
    {
        currentTime += TimeSpan.FromMinutes(1);             // add one minute
        //Debug.Log("JEST TERAZ CZAS: " + currentTime);
        gameClock.SetText(currentTime.ToString(@"hh\:mm"));
        yield return new WaitForSeconds(oneMinuteLength);   // wait for one minute in game
        StartCoroutine(MinutePassed());                     //infinite loop
    }
 

    private float PercentOFDay(TimeSpan timespan)
    {
        return (float)timespan.TotalMinutes % 1440 / 1440;   //when 1.15 make 0.15
    }
}
