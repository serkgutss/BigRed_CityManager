using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public static DayNightSystem instance;
    public float currentTime;
    public float dayLengthMinutes;

    public TextMeshProUGUI timeText;

    float midday;
    float translateTime;
    string AMPM = "AM";

    float rotationSpeed;



    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        midday = dayLengthMinutes * 60 / 2;
        rotationSpeed = 360 / dayLengthMinutes / 60;
    }



    private void Update()
    {
        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24;

        float hours = Mathf.Floor(t);

        string displayHours = hours.ToString();
        if (hours == 0)
        {
            displayHours = "00";
        }
        if (hours > 12)
        {
            displayHours = (hours - 12).ToString();
        }
        if (currentTime >= midday)
        {
            if (AMPM != "PM")
            {
                AMPM = "PM";
            }


        }
        if (currentTime >= midday * 2)
        {

            
            if (AMPM != "AM")
            {
                AMPM = "AM";
            }
            City.instance.EndTurn();
            currentTime = 0;
        }

        t *= 60;

        float minutes = Mathf.Floor(t % 60);

        string displayMinutes = minutes.ToString();

        if (minutes < 10)
        {
            displayMinutes = "0" + minutes.ToString();
        }
        string displayTime = displayHours + ":" + displayMinutes + " " + AMPM;

        timeText.text = displayTime;

        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }

    public void ResetTimer()
    {

        currentTime = 0;
    }
}
