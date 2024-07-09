using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public enum ZombieType
{
    Ordinary,

}

public enum ZombieState
{
    Move,
    Attack
}

public class Zombie : MonoBehaviour
{
    protected int _hp;
    [SerializeField]
    protected int MaxHP;
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

    public int AttackPoint = 10;
    public float MoveSpeed = 1f;
    public Vector2 MoveDirection;
    public ZombieType Type;
    public ZombieState State;
    public EventHandler<ZombieType> ZombieDeathEvent;

    protected GameObject _currentAttakTarget;
    protected Animator _animator;
    [SerializeField]
    protected GameObject _head;
    protected Rigidbody2D _rigidBody;

    protected HitEffect hitEffect;   

    public void AdjustSort()
    {

    }

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        HP = MaxHP;
        _head.SetActive(false);
        hitEffect = GetComponent<HitEffect>();
        //GetComponent<SpriteRenderer>().sortingOrder += (int)(transform.position.y*100);
    }

    public void Hurt(int damage)
    {
        HP -= damage;
        hitEffect.EffectOnce();
    }

    public void Death()
    {
        //ZombieDeathEvent.Invoke(this,Type);
        //PlayAnim
        _animator.SetTrigger("die");
        _head.SetActive(true);
        _head.transform.SetParent(null); //dispatch head
        //PlaySound
        
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
        Destroy(_head);
    }

    virtual protected void MoveUpdate()
    {
        _animator.SetBool("should_attack",false);
        _rigidBody.MovePosition(_rigidBody.position + new Vector2(MoveDirection.x,MoveDirection.y) * MoveSpeed * Time.deltaTime);
    }
    virtual protected void AttackUpdate()
    {
        _rigidBody.velocity = Vector2.zero;
        _animator.SetBool("should_attack", true);
        if (_currentAttakTarget == null)
        {
            State = ZombieState.Move;
            return;
        }
    }
    virtual public void AttackOnce()
    {
        if (_currentAttakTarget != null)
        {
            _currentAttakTarget.GetComponent<Plant>().Hurt(AttackPoint);
            GetComponent<AudioSource>().Play();
        }
        
    }
    // Update is called once per frame
    virtual protected void Update()
    {
        switch(State)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Attack:
                AttackUpdate();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Plant>(out var plant))
        {
            State = ZombieState.Attack;
            _currentAttakTarget = plant.gameObject;
        }
    }

}
