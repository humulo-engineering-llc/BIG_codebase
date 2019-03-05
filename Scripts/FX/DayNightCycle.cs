using UnityEngine;
             //
public class DayNightCycle : MonoBehaviour
{
    public float dayLength;

    //--Morning
    public float morningStart, morningEnd;
    public Color morningSunColor_sunny;
    public Color morningSunColor_rain;
    //--Midday
    public Color middaySunColor_sunny;
    public Color middaySunColor_rain;
    //--Evening
    public float eveningStart, eveningEnd;
    public Color eveningSunColor_sunny;
    public Color eveningSunColor_rain;

    //--Sun angle tracker
    float sunAngle;
    public string curState;

    //--Light var
    Light myLight;

    //--Weather tracking
    Weather weather;

    void Start()
    {
        myLight = this.GetComponent<Light>();

        //--Get time of day
        sunAngle = this.transform.eulerAngles.x;
        //--Get weather
        weather = GameObject.Find("Weather").GetComponent<Weather>();
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, dayLength * Time.deltaTime);
        sunAngle += Time.deltaTime * dayLength;
        if(sunAngle >= 360f)
        {
            sunAngle = 0f;
        }
    }

    void LateUpdate()
    {
        if (sunAngle >= morningStart && sunAngle < morningEnd)
        {
            //--Morning
            curState = "dawnDusk";
            if(weather.curWeather == "Sunny")
            {
                myLight.color = Color.Lerp(myLight.color, morningSunColor_sunny, 0.005f);
            }
            if(weather.curWeather == "Rain")
            {
                myLight.color = Color.Lerp(myLight.color, morningSunColor_rain, 0.005f);
            }
            
        }
        if(sunAngle >= morningEnd && sunAngle < eveningStart)
        {
            //---Midday
            curState = "day";
            if (weather.curWeather == "Sunny")
            {
                myLight.color = Color.Lerp(myLight.color, middaySunColor_sunny, 0.005f);
            }
            if (weather.curWeather == "Rain")
            {
                myLight.color = Color.Lerp(myLight.color, middaySunColor_rain, 0.005f);
            }
            
        }
        if (sunAngle >= eveningStart && sunAngle < eveningEnd)
        {
            //--Evening
            curState = "dawnDusk";
            if (weather.curWeather == "Sunny")
            {
                myLight.color = Color.Lerp(myLight.color, eveningSunColor_sunny, 0.005f);
            }
            if (weather.curWeather == "Rain")
            {
                myLight.color = Color.Lerp(myLight.color, eveningSunColor_rain, 0.005f);
            }
            
        }
        if(sunAngle >= eveningEnd)
        {
            curState = "night";
        }
    }
}
