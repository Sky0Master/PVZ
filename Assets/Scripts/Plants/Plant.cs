using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum PlantState
{
    Cooling,
    Ready
}

public class Plant : MonoBehaviour
{
   
    public int Cost = 100;
    public PlantState State;
    public float Cd = 1f;
    
    [SerializeField]
    protected int _maxHp = 50;
    protected int _hp = 10;
    protected Animator _animator;
    
    private float _timer;
    private HitEffect _hitEffect;
    public int HP
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                value = 0;
                Death();
            }
            _hp = value;
        }

    }
    virtual protected void Awake()
    {
        _animator = GetComponent<Animator>();
        if (!TryGetComponent<HitEffect>(out _hitEffect))
        {
           _hitEffect = gameObject.AddComponent<HitEffect>();
        }

    }
    protected void Start()
    {
        _timer = Cd;
    }

    public void Hurt(int damage)
    {
        HP -= damage;
        _hitEffect.EffectOnce();
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    
    virtual protected void CoolingUpdate()
    {
        _timer -= Time.deltaTime;
        if( _timer <= 0 )
        {
            _timer = Cd;
            State = PlantState.Ready;
        }
    }
    virtual protected void ReadyUpdate()
    {
        
    }
    protected void Update()
    {
        switch (State)
        {
            case PlantState.Cooling:
                CoolingUpdate();
                break;
            case PlantState.Ready:
                ReadyUpdate();
                break;
        }
    }
}
