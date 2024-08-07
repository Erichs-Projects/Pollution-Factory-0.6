using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class SunController : MonoBehaviour
{
    public PlayerStats playerStats;
    public int SunSpeedMultiplier = 10;
    public float MoneyUpdateFrequency = 1f;

    private float lastDay = 0f;
    private float timeOfDay;
    private float timeOfLastUpdate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Keeps track of current time in the day
        // float timeOfDay = Time.time * SunSpeedMultiplier - lastDay * SunSpeedMultiplier;
        timeOfDay = (Time.time - lastDay) * SunSpeedMultiplier;

        // Rotate Sun according to time of day
        transform.eulerAngles = new Vector3(timeOfDay, 0, 0);

        // If sun goes all around, increase day by one
        if ((Time.time - lastDay) >= (360 / SunSpeedMultiplier))
        {
            lastDay = Time.time;
            playerStats.increaseDayByOne();
        }

        if ((Time.time - timeOfLastUpdate) >= MoneyUpdateFrequency)
        {
            // print("<color=red>" + Time.time + "</color>");
            playerStats.UpdateMoney();
            timeOfLastUpdate = Time.time;
        }

        if (transform.rotation.x >= 1)
        {
            gameObject.GetComponent<Light>().intensity = 0f;
            playerStats.isEvening = true;
        }
        if (transform.rotation.x <= 0.1)
        {
            gameObject.GetComponent<Light>().intensity = 1f;
            playerStats.isEvening = false;
        }
    }

    float GetTimeOfDay()
    {
        return timeOfDay;
    }

    // Reset Sun to original position
    public void ResetSun()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        lastDay = Time.time;
    }
}
