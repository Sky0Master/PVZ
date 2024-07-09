using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int _currentLevel;

    [SerializeField]
    SceneAsset[] _scenes;

    public void NextLevel()
    {
        //scenes[_currentLevel].
        _currentLevel++;
        SceneManager.LoadScene(_scenes[_currentLevel].name);
    }

}
