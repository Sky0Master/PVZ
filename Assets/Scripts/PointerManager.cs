using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerManager : MonoBehaviour
{
    public GameObject CurrentHoverGameObject;

    //public GameObject CurrentHoverPlant;
    //public GameObject CurrentHoverCard;
    [SerializeField] float clickRadius = 1f;

    private bool _isSnapping;
    public bool IsSnapping {
        get => _isSnapping;
    }
    public GameObject CurrentSnappingGameObject;
    [SerializeField] Image _snappingImage;
    public void SetSnapping(GameObject snapGo, Sprite spr)
    {
        if (CurrentSnappingGameObject != null)
            CancelSnapping();
        _isSnapping = true;
        CurrentSnappingGameObject = snapGo;
        _snappingImage.sprite = spr;
        _snappingImage.enabled = IsSnapping;

    }
    public void CancelSnapping()
    {
        _isSnapping = false;
        if (CurrentSnappingGameObject != null && CurrentSnappingGameObject.TryGetComponent<Card>(out var card))
        {
            card.IsSnapping = false;
        }
        CurrentSnappingGameObject = null;
        _snappingImage.enabled = false;
    }
    public bool IsHoverGrid()
    {
        return false;
    }
    public GameObject GetHoverGameObject()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var res = Physics2D.OverlapCircleAll(pos, clickRadius);
        CurrentHoverGameObject = null;
        foreach (var collider in res)
        {
            if (collider.gameObject.GetComponent<Block>())
            {
                if(CurrentHoverGameObject == null || 
                    Vector2.Distance(CurrentHoverGameObject.transform.position,pos) > Vector2.Distance(collider.transform.position,pos))
                    CurrentHoverGameObject = collider.gameObject;
               
            }
        }
        
        
        return CurrentHoverGameObject;
    }
    public void AdjustSnapPosition()
    {
        if (CurrentHoverGameObject == null)
        {
            _snappingImage.transform.position = Input.mousePosition;
        }
        else
        {
            _snappingImage.transform.position = Camera.main.WorldToScreenPoint(CurrentHoverGameObject.transform.position);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CancelSnapping();
        }
        GetHoverGameObject();
        AdjustSnapPosition();
        if (CurrentHoverGameObject!=null && IsSnapping && Input.GetMouseButtonDown(0))
            CurrentSnappingGameObject.GetComponent<Card>().OnClickWhileSnapping();

    }
}
