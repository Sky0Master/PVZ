using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    [SerializeField] private GameObject peaPrefab;
    [SerializeField] Transform peaGeneratePos;
    [SerializeField] float detectRange = 100;
    public bool ShouldFire()
    {
        var results = Physics2D.RaycastAll(peaGeneratePos.position,transform.right,detectRange);
        foreach(var res in results)
        {
            if(res.collider.TryGetComponent<Zombie>(out var target))
            {
                return true;
            }
        }
        return false;
    }

    protected override void ReadyUpdate()
    {
        base.ReadyUpdate();
        if (ShouldFire())
        {
            var peaGo = GameObject.Instantiate(peaPrefab);
            peaGo.transform.position = peaGeneratePos.position;
            peaGo.GetComponent<Pea>().Setup(transform.right);
            GetComponent<AudioSource>().Play();
            State = PlantState.Cooling;
        }
    }
}
