using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SunManager : MonoBehaviour
{
    [SerializeField]
    private float _dropInterval;

    [SerializeField]
    private Transform _leftBottom;
    [SerializeField] 
    private Transform _rightTop;

    [SerializeField]
    private int _sunCount = 0;

    [SerializeField]
    private GameObject _sunPrefab;

    public EventHandler<int> SunNumberChangeEvent;

    float _timer = 0;
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer>_dropInterval)
        {
            _timer = 0;
            DropSunRandom();
        }
    }

    public int SunCount
    {
        get => _sunCount;
        set
        {
            if (value < 0)
                value = 0;
            _sunCount = value;
            SunNumberChangeEvent.Invoke(this, value);
        }
        
    }

    public void DropSunRandom()
    {

    }
    private void Start()
    {
        SunNumberChangeEvent.Invoke(this, SunCount);
    }
    public void GainSun(int value)
    {
        SunCount += value;
    }
    public bool UseSun(int value)
    {
        if (SunCount < value)
            return false;
        SunCount -= value;
        return true;
    }

}
