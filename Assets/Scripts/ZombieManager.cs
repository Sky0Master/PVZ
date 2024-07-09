using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{

    [SerializeField] Transform[] generatePos;
    [SerializeField] float generateInterval = 3f;
    [SerializeField] GameObject[] zombiePrefabs;
    float _timer = 0;
    public Vector3 GetRandomGeneratorPos()
    {
        var index = UnityEngine.Random.Range(0, generatePos.Length);
        return generatePos[index].position;
    }

    public GameObject BuildZombie(GameObject zombiePrefab,Vector3 pos)
    {
        var zombieGo = GameObject.Instantiate(zombiePrefab);
        zombieGo.transform.position = pos;
        zombieGo.GetComponent<SpriteRenderer>().sortingOrder = 1000 - (int)pos.y * 100;
        return zombieGo;
    }
    public GameObject BuildZombieAtRandomPos(GameObject zombiePrefab)
    {
        return BuildZombie(zombiePrefab,GetRandomGeneratorPos());
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            var index = UnityEngine.Random.Range(0, zombiePrefabs.Length);
            if (zombiePrefabs.Length > 0)
            {
                BuildZombieAtRandomPos(zombiePrefabs[index]);
            }
            _timer = generateInterval;
        }
    }

}
