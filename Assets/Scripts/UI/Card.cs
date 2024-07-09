using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardState
{
    Cooling,
    Ready
}

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Sprite _readyIcon;
    [SerializeField] protected Sprite _unavailableIcon;
    [SerializeField] protected Sprite _snappingSprite;
    [SerializeField] protected PointerManager _pointerManager;
    [SerializeField] protected Image _mask;
    public bool CanClick = true;
    public int Cost;
    public float Cd = 5f;
    protected bool _isSnapping = false;

    protected CardState _state;
    public CardState State {
        get => _state;
        set
        {
            if(value == CardState.Cooling)
            {
                _cdTimer = Cd;
                Debug.Log($"{gameObject.name} cooling");
            }
            _state = value;
        }
    }

    protected float _cdTimer = 0;
    public bool IsSnapping {
        get => _isSnapping;
        set
        {
            _isSnapping = value;
            OnIsSnappingChange();
        }
    }
    private void FixedUpdate()
    {
        if (_cdTimer > 0)
        {
            Debug.Log(_cdTimer);
            _cdTimer = _cdTimer - Time.fixedDeltaTime;
        }
        if (_cdTimer <= 0)
        {
            _cdTimer = 0;
            State = CardState.Ready;
        }
    }

    virtual protected void Update()
    {
        
    }

    private void OnIsSnappingChange()
    {
        if (IsSnapping)
        {
            _pointerManager.SetSnapping(gameObject, _snappingSprite);
        }
    }

    private void Awake()
    {
        if (_pointerManager == null)
        {
            _pointerManager = GameObject.Find("PointerManager").GetComponent<PointerManager>();
        }
        State = CardState.Ready;
        if(transform.Find("mask"))
        {
            _mask = transform.Find("mask").GetComponent<Image>();
        }
    }

    public virtual void OnClickWhileSnapping() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanClick)
        {
            return;
        }
        if (!IsSnapping && eventData.button == PointerEventData.InputButton.Left)
        {
            IsSnapping = true;
            return;
        }
        
    }
    
}
