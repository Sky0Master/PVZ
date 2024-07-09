using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunNumberDisplay : MonoBehaviour
{
    [SerializeField] SunManager sunManager;
    TextMeshProUGUI numText;
    private void Awake()
    {
        sunManager = GameObject.Find("SunManager").GetComponent<SunManager>();    
        numText = GetComponent<TextMeshProUGUI>();
        sunManager.SunNumberChangeEvent += OnNumberChange;
        numText.SetText(sunManager.SunCount.ToString());
    }

    private void OnNumberChange(object sender, int e)
    {
        numText.SetText(e.ToString());
    }

}
