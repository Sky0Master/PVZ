using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GameObject gridObjectPrefab;
    [SerializeField] float cellSize;
    Vector3 originalPos;
    Vector3 leftTop;
    Vector3 rightTop;
    Vector3 bottomRight;
    GameObject[,] grid;

    void DestroyAllChildrenInEditor(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.parent == parent.transform)
            {
                Undo.DestroyObjectImmediate(child.gameObject);
            }
        }
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        if (gridObjectPrefab == null)
            return;
        DestroyAllChildrenInEditor(gameObject);
        originalPos = transform.position;
        leftTop = originalPos + new Vector3(0, cellSize * height, 0);
        rightTop = originalPos + new Vector3(cellSize * width, cellSize * height, 0);
        bottomRight = originalPos + new Vector3(cellSize * width, 0, 0);
        grid = new GameObject[width, height];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = PrefabUtility.InstantiatePrefab(gridObjectPrefab,transform) as GameObject;
                grid[i, j].transform.position = (new Vector3(i, j) * cellSize) + originalPos;
                grid[i, j].name = $"block_{i}_{j}";
            }
        }

    }

}
