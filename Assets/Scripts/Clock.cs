using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float hoursToDegrees = -30f;
    private const float minutesToDegrees = -6f;
    private const float secondsToDegrees = -6f;

    [SerializeField] private Transform hoursPivot;
    [SerializeField] private Transform minutesPivot;
    [SerializeField] private Transform secondsPivot;

    private void Awake() //should be executed when the component awakens.
    {

    }
    // When a component has an Awake method, Unity will invoke that method on the component when it awakens.
    // This happens after it's been created or loaded while in play mode. We're currently in edit mode,
    // so this doesn't happen yet.
    //
    //The result of this invocation is a Quaternion struct value containing a 30° clockwise rotation
    //around the Z axis, matching hour 1 on our clock.
    //invoked by Unity just once


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondsToDegrees * (float)time.TotalSeconds);
    }
}

