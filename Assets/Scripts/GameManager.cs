using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager
{
    private static GameManager instance;
    private string _currentCanvas;
    [SerializeField]
    private float _maxLife;
    private float _currentLife;
    public float CurrentLife
    {
        get => _currentLife;
        set
        {
            _currentLife = value;
            if (_currentLife <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public float MaxLife { get => _maxLife; }
    private GameManager()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        _maxLife = 2;
        _currentLife = MaxLife;
    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    public void SwitchScene(string currentCanvas, string targetScene)
    {
        _currentCanvas = currentCanvas;
        Debug.Log(targetScene);
        SceneManager.LoadScene(targetScene);
    }
    public void SwitchScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "CutScene":
                if (_currentCanvas == null) _currentCanvas = "CanvasS9";
                GameObject.Find("CanvasS1").GetComponent<CanvasGroup>().Hide();
                GameObject.Find(_currentCanvas).GetComponent<CanvasGroup>().Show();
                break;
            default:
                _maxLife = 2;
                _currentLife = MaxLife;
                break;
        }
    }
}
