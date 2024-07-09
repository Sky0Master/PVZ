using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDefinition : ScriptableObject
{
    public string Name;
    public int Cost;
    [SerializeField] GameObject prefab;
    
}
