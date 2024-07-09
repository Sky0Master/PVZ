using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private SunManager _sunManager;
    private Vector3 _sunDestinyPos;
    private Collider2D _sunCollider;
    public int Value = 25;
    public float Speed = 1f;
    Rigidbody2D _rigidBody;

    private void Awake()
    {
        _sunDestinyPos = GameObject.Find("SunDestiny").transform.position;
        _sunManager = GameObject.Find("SunManager").GetComponent<SunManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _sunCollider = GetComponent<Collider2D>();
        _sunCollider.enabled = true;
    }

    void Update()
    {
        // ����������Ƿ񱻰���
        if (Input.GetMouseButtonDown(0))
        {
            // ����������λ���Ƿ���GameObject�ı߽���
            if (IsPointerOverGameObject(gameObject))
            {
                _rigidBody.velocity = Speed * (_sunDestinyPos - transform.position);
                _sunManager.GainSun(Value);
                GetComponent<AudioSource>().Play();
            }
        }
        
    }
    // ������ָ���Ƿ���GameObject��
    bool IsPointerOverGameObject(GameObject target)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.RaycastAll(ray.origin, ray.direction, 100);
        foreach (var hitObj in hit)
        {
            if (hitObj.collider.gameObject == target)
            {
                return true;
            }
        }
        return false;
    }

}
