using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _duration = 0.05f;
    [SerializeField] private float _minAlpha = 0.3f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator<WaitForSeconds> Blink(SpriteRenderer spr, float duration, float minAlpha)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            var newAlpha = Mathf.Lerp(1, minAlpha, timer / duration);
            spr.color = new Color(1, 1, 1, newAlpha);
            yield return new WaitForSeconds(0);
        }
        while(timer>0)
        {
            timer -= Time.deltaTime;
            var newAlpha = Mathf.Lerp(1, minAlpha, timer / duration);
            spr.color = new Color(1, 1, 1, newAlpha);
            yield return new WaitForSeconds(0);
        }
        
    }
    public void EffectOnce()
    {
        Coroutine coroutine = StartCoroutine(Blink(_spriteRenderer, _duration, _minAlpha));
    }
}
