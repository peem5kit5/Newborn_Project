using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public ThemeSet_SO Theme;

    public GameObject[] Walls;
    public GameObject[] Doors;
    public GameObject[] Environments;
    public GameObject[] Entity;

    public float XsizeForEnvironments;
    public float ZsizeForEnvironments;
    public int EnvironmentRNG;

    private Transform Parent;
    public void UpdateRoom(bool[] _status)
    {
        if(ThemeHolder.Instance.CurrentThemeSet != Theme)
        {
            Debug.LogError("Wrong Theme.");
            return;
        }

        Parent = this.transform;
        for(int i = 0; i < _status.Length; i++)
        {
            if(Doors.Length >= _status.Length)
                Doors[i].SetActive(_status[i]);
            if(Walls.Length >= _status.Length)
                Walls[i].SetActive(!_status[i]);

            if (_status[i])
            {
                GenerateEnvironments(i);
            }
        }
    }

    public void GenerateEnvironments(int _index)
    {
        for (int i = 0; i < EnvironmentRNG; i++)
        {
            int _rng = Random.Range(0, Environments.Length);
            Vector3 randomPosition = GetRandomPosition(_index);
            Instantiate(Environments[_rng], randomPosition, Quaternion.identity, Parent);
        }
    }

    private Vector3 GetRandomPosition(int _index)
    {
        float x, z;
        float _minDistanceCenter = 2f;

        do
        {
            x = Random.Range(-5f, 5f);
            z = Random.Range(-5f, 5f);
        }
        while (Mathf.Abs(x) < _minDistanceCenter && Mathf.Abs(z) < _minDistanceCenter);

        Vector3 _doorPos = Doors[_index].transform.position;
        Vector3 _envPos = new Vector3(x, 0f, z) + _doorPos;

        return _envPos;
    }
}
