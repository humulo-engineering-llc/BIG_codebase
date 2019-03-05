using UnityEngine;
            //
public class Weather : MonoBehaviour
{
    //--Weather master
    public string curWeather;
    //--Game objects for weather patterns
    //--Rain
    public GameObject rain;
    float rainRate;
    //--Fader
    //--Use for all fade purposes to synchronize
    public float fader;
    //--Fog
    public Color clearFog_day;
    public Color clearFog_dawnDusk;
    public Color clearFog_night;
    public Color cloudyFog_day;
    public Color cloudyFog_dawnDusk;
    public Color cloudyFog_night;
    //--Min and max time for season change
    public Vector2 minAndMaxTimes;
    //--Timer for change
    float timer;
    //--Trigger time
    float trigger;
    //--Transition Rate
    public float fadeRate;
    //--Sun
    public Light sun;
    DayNightCycle dayTracker;
    public float maxSunLevel;

    //--clouds
    public GameObject lowClouds, highClouds;
    float currentDensity_low, currentDensity_high;
    //--Low Clouds
    public Color day_stormyCloudColor_low, day_sunnyCloudColor_low;
    public Color dawnDusk_stormyCloudColor_low, dawnDusk_sunnyCloudColor_low;
    public Color night_stormyCloudColor_low, night_sunnyCloudColor_low;
    //--High Clouds
    public Color day_stormyCloudColor_high, day_sunnyCloudColor_high;
    public Color dawnDusk_stormyCloudColor_high, dawnDusk_sunnyCloudColor_high;
    public Color night_stormyCloudColor_high, night_sunnyCloudColor_high;

    Color cloudColor_low, cloudColor_high;

    public bool Running;
    float blender;

    void Start()
    {
        sun = GameObject.Find("Sun").GetComponent<Light>();
        timer = 0f;
        newTime();
        curWeather = "Sunny";
        blender = 0f;
        sun.intensity = 1.6f;
        dayTracker = sun.gameObject.GetComponent<DayNightCycle>();

        //--Set rain state
        var emission = rain.GetComponent<ParticleSystem>().emission;
        rainRate = 0f;
        emission.rateOverTime = rainRate;
        rain.GetComponent<ParticleSystem>().Play();

        //--Set fog
        RenderSettings.fogColor = clearFog_day;
        RenderSettings.fogEndDistance = 650f;
        fader = 1f;

        //--Clouds
        currentDensity_low = 1.5f;
        currentDensity_high = 1.7f;

        cloudColor_low = day_sunnyCloudColor_low;
        cloudColor_high = day_sunnyCloudColor_high;
    }

    void LateUpdate()
    {
        //--clamp blend rate to improve reliability
        blender = Mathf.Clamp(blender, 0f, 1f);
        //--Clamp sun intensity
        sun.intensity = Mathf.Clamp(sun.intensity, 0.6f, maxSunLevel);

        var emission = rain.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = rainRate;

        //--Cloud control
        currentDensity_low = Mathf.Clamp(currentDensity_low, 0.6f, 1.75f);
        currentDensity_high = Mathf.Clamp(currentDensity_high, 0f, 1.7f);
        lowClouds.GetComponent<MeshRenderer>().material.SetFloat("_Density", currentDensity_low);
        highClouds.GetComponent<MeshRenderer>().material.SetFloat("_Density", currentDensity_high);

        lowClouds.GetComponent<MeshRenderer>().material.SetColor("_CloudColor", cloudColor_low);
        highClouds.GetComponent<MeshRenderer>().material.SetColor("_CloudColor", cloudColor_high);

        if (Running)
        {
            timer += Time.deltaTime;
            if (timer >= trigger)
            {
                newTime();
                CycleWeather();
                timer = 0f;
            }

            if(curWeather == "Rain")
            {
                //--Sun
                sun.intensity -= Time.deltaTime * (fadeRate/2f);

                //--Fog
                if (dayTracker.curState == "day")
                {
                    RenderSettings.fogColor = Color.Lerp(clearFog_day, cloudyFog_day, fader);
                }
                if (dayTracker.curState == "dawnDusk")
                {
                    RenderSettings.fogColor = Color.Lerp(clearFog_dawnDusk, cloudyFog_dawnDusk, fader);
                }
                if (dayTracker.curState == "night")
                {
                    RenderSettings.fogColor = Color.Lerp(clearFog_night, cloudyFog_night, fader);
                }

                if (fader < 1f)
                {
                    fader += Time.deltaTime * fadeRate;
                }
                if(RenderSettings.fogEndDistance >= 250f)
                {
                    RenderSettings.fogEndDistance -= Time.deltaTime * 15f;
                }

                //--Clouds
                currentDensity_low -= Time.deltaTime * (fadeRate / 2f);
                currentDensity_high -= Time.deltaTime * (fadeRate / 2f);

                if(dayTracker.curState == "day")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, day_stormyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, day_stormyCloudColor_high, fadeRate / 50f);
                }
                if(dayTracker.curState == "dawnDusk")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, dawnDusk_stormyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, dawnDusk_stormyCloudColor_high, fadeRate / 50f);
                }
                if(dayTracker.curState == "night")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, night_stormyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, night_stormyCloudColor_high, fadeRate / 50f);
                }

                //--Rain
                if (currentDensity_low <= 0.65f)
                {
                    if (rainRate <= 1000f)
                    {
                        rainRate += Time.deltaTime * 75f;
                    }
                }
            }
            if(curWeather == "Sunny")
            {
                //--Sun
                sun.intensity += Time.deltaTime * (fadeRate/2f);

                //--Fog
                if (dayTracker.curState == "day")
                {
                    RenderSettings.fogColor = Color.Lerp(cloudyFog_day, clearFog_day, fader);
                }
                if (dayTracker.curState == "dawnDusk")
                {
                    RenderSettings.fogColor = Color.Lerp(cloudyFog_dawnDusk, clearFog_dawnDusk, fader);
                }
                if (dayTracker.curState == "night")
                {
                    RenderSettings.fogColor = Color.Lerp(cloudyFog_night, clearFog_night, fader);
                }
                if(fader < 1f)
                {
                    fader += Time.deltaTime * fadeRate;
                }
                if(RenderSettings.fogEndDistance <= 650f)
                {
                    RenderSettings.fogEndDistance += Time.deltaTime * 15f;
                }

                //--Rain
                if (rainRate >= 0f)
                {
                    rainRate -= Time.deltaTime * 75f;
                }

                //--Clouds
                currentDensity_low += Time.deltaTime * (fadeRate / 2f);
                currentDensity_high += Time.deltaTime * (fadeRate / 2f);

                if(dayTracker.curState == "day")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, day_sunnyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, day_sunnyCloudColor_high, fadeRate / 50f);
                }
                if(dayTracker.curState == "dawnDusk")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, dawnDusk_sunnyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, dawnDusk_sunnyCloudColor_high, fadeRate / 50f);
                }
                if(dayTracker.curState == "night")
                {
                    cloudColor_low = Color.Lerp(cloudColor_low, night_sunnyCloudColor_low, fadeRate / 50f);
                    cloudColor_high = Color.Lerp(cloudColor_high, night_sunnyCloudColor_high, fadeRate / 50f);
                }
            }

            
        }
    }

    //--Reset trigger time
    public void newTime()
    {
        trigger = Random.Range(minAndMaxTimes.x, minAndMaxTimes.y);
    }
    
    //--Chooses random weather cycle
    public void RandomWeather()
    {
        float decider = Random.Range(0.0f, 1.0f);
        if(decider < 0.5f)
        {
            curWeather = "Rain";
            fader = 0f;
        } else
        {
            curWeather = "Sunny";
            fader = 0f;
        }
    }

    //--Cycles from list
    public void CycleWeather()
    {
        switch (curWeather)
        {
            case ("Rain"):
                curWeather = "Sunny";
                fader = 0f;
                break;
            case ("Sunny"):
                curWeather = "Rain";
                fader = 0f;
                break;
        }
    }

}
