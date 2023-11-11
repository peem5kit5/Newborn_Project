using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using TMPro;
public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance { get; private set; }
    public enum DayNight { Day, Night }
    [Header("Day Setting")]
    public DayNight DayTime;
    [Space]
    [Header("Light Info")]
    public List<Light> Lights = new List<Light>();
    [Space]
    [Header("Times")]
    public float CurrentTime;
    public float TimeToChange;
    public TextMeshProUGUI TimeText;

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
    }
    public void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        //FindAllLight();
    }
    public void FindAllLight()
    {
        Light[] _lights = FindObjectsOfType<Light>();
        Lights = _lights.ToList();
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
