using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantCard : Card
{
    [SerializeField] protected GameObject _plantPrefab;
    Image _img;

    [SerializeField] protected SunManager _sunManager;

    private void OnValidate()
    {
        if (_plantPrefab != null)
        {
            Cost = _plantPrefab.GetComponent<Plant>().Cost;
            _snappingSprite = _plantPrefab.GetComponent<SpriteRenderer>().sprite;
        }
        if(_readyIcon != null)
        {
            _img = GetComponent<Image>();
            _img.sprite = _readyIcon;
        }
    }

    override protected void Update()
    {
        base.Update();
        _mask.fillAmount = _cdTimer / Cd;
        CanClick = (State == CardState.Ready && _img.sprite == _readyIcon);
    }
    private void Start()
    {
        _sunManager = GameObject.Find("SunManager").GetComponent<SunManager>();
        _sunManager.SunNumberChangeEvent += OnSunChange;
    }

    private void OnSunChange(object sender, int e)
    {
        if (e >= Cost)
        {
            _img.sprite = _readyIcon;
        }
        else
        {
            _img.sprite = _unavailableIcon;
        }
    }

    public override void OnClickWhileSnapping()
    {
        var block = _pointerManager.CurrentHoverGameObject.GetComponent<Block>();
        if (block != null)
        {
            block.CreatePlantByPrefab(_plantPrefab);
            State = CardState.Cooling;
            _sunManager.UseSun(Cost);
        }
        _pointerManager.CancelSnapping();
    }
}
