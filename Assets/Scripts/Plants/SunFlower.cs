using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{

    [SerializeField] GameObject sunPrefab;
    SunManager sunManager;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _lightCo;
    public float LightDuration = 1f;
    public Color NormalColor;
    public Color LightColor;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = NormalColor;
        
    }
    protected override void ReadyUpdate()
    {
        base.ReadyUpdate();
        Light(LightDuration);
        State = PlantState.Cooling;
    }
    public void ProduceSun()
    {
        var sunGo = GameObject.Instantiate(sunPrefab);
        var offset = new Vector3(0.1f,0.1f,0);
        sunGo.transform.position = transform.position + offset;
    }

    private IEnumerator<WaitForSeconds> Lighting(float duration, SpriteRenderer spr, Color startColor,Color endColor)
    {
        float timer = 0f;
        //float delta = ((endValue - startValue) / duration) * Time.deltaTime;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            spr.color = Color.Lerp(startColor, endColor, timer/duration);
            yield return new WaitForSeconds(0);
        }
        ProduceSun();
        spr.color = NormalColor;
    }
    public void Light(float duration)
    {
        Debug.Log("Light!");
        _lightCo = StartCoroutine(Lighting(duration,_spriteRenderer,NormalColor,LightColor));
        
    }

}
