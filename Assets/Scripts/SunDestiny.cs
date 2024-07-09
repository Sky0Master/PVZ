using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDestiny : MonoBehaviour
{
    [SerializeField]
    Transform SunNumber;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(SunNumber.position);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Sun>(out var sun))
        {
            Destroy(sun.gameObject);
        }
    }
}
