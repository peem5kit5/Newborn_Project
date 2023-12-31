using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using TMPro;
public class DayNightManager : Singleton<DayNightManager>
{
    public enum DayNight { Day, Night }
    [Header("Day Setting")]
    public DayNight DayTime;
    [Space]
    [Header("Light Info")]
    public Transform LightParent;
    public List<Light> Lights = new List<Light>();
    public Color DayLight;
    public Color NightLight;
    [Space]
    [Header("Times")]
    public float CurrentTime;
    public float TimeToChange;
    public TextMeshProUGUI TimeText;

    private Color currentColor;
    private Color targetColor;

    bool changingLight;
    public void ChangeDayTime()
    {
        if(DayTime == DayNight.Day)
        {
            DayTime = DayNight.Night;
        }
        else
        {
            DayTime = DayNight.Day;
        }

        if(!changingLight)
            UpdateLight();

    }
    public bool NightTime()
    {
        return DayTime != DayNight.Day;
    }
    public override void Awake()
    {
        base.Awake();
        Init();
    }
    public void Init()
    {
        //FindAllLight();
        UpdateLight();
    }
    private IEnumerator UpdateLightCoroutine(Light[] _lights)
    {
        while (true)
        {
            if (NightTime())
            {
                targetColor = NightLight;
            }
            else
            {
                targetColor = DayLight;
            }

            float elapsedTime = 0f;
            while (elapsedTime < 10)
            {
                currentColor = Color.Lerp(currentColor, targetColor, elapsedTime / 10);
                for (int i = 0; i < _lights.Length; i++)
                {
                    _lights[i].color = currentColor;
                }
                elapsedTime += Time.deltaTime;
                changingLight = false;
                yield return null;
            }
            currentColor = targetColor;
            changingLight = false;
            yield return null;
        }

    }
    public void UpdateLight()
    {

        Light[] _lights = LightParent.GetComponentsInChildren<Light>();
        if(_lights.Length > 0)
        {
            if (NightTime())
            {
                    targetColor = NightLight;
            }
            else
            {
                    targetColor = DayLight;
            }
        }

        StartCoroutine(UpdateLightCoroutine(_lights));
    }


    public void ChangeSceneLight()
    {
        Lights.Clear();
    }

    public void Update()
    {
        Timer();
    }

    void CheckingTime()
    {
        if(CurrentTime >= TimeToChange)
        {
            ChangeDayTime();
            CurrentTime = 0;
        }
    }
    void Timer()
    {
        CurrentTime += Time.deltaTime;
        float _minutes = Mathf.FloorToInt(CurrentTime / 60);
        float _seconds = Mathf.FloorToInt(CurrentTime % 60);

        TimeText.text = string.Format("{0:00} : {1:00}", _minutes, _seconds);
        CheckingTime();
    }
}
