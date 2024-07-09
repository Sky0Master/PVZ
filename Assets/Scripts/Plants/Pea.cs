using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pea : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    public float LifeTime = 5f;
    public int Damage = 10;
    public float Speed = 5f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Zombie>(out var zombie))
        {
            Boom();
            zombie.Hurt(Damage);
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void Boom()
    {
        animator.enabled = true;
        rigidBody.velocity = Vector3.zero;
        GetComponent<AudioSource>().Play();
    }
    public void Setup(Vector2 direction)
    {
        rigidBody.velocity = direction * Speed;
        Invoke("Boom",LifeTime);
    }
    
}
