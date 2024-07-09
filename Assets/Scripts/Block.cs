using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private GameObject plant;
    public bool CanPlant = true;
    
    public void CreatePlantByPrefab(GameObject plantPrefab)
    {
        if (plant != null) return;
        plant = GameObject.Instantiate(plantPrefab);
        plant.transform.position = transform.position;
        GetComponent<AudioSource>().Play();
    }


    public void RemovePlant()
    {
        if(plant!=null)
        {
            Destroy(plant);
            plant = null;
            GetComponent<AudioSource>().Play();
        }
    }
    

    void Update()
    {
        CanPlant = (plant == null);
    }
}
