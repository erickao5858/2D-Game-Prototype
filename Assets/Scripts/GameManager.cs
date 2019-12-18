using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
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
                SceneManager.LoadScene("CutScene");
            }
        }
    }
    public float MaxLife { get => _maxLife; }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (_maxLife == 0) _maxLife = 1;
        _currentLife = MaxLife;
    }
    public void SwitchScene(string currentCanvas, string targetScene)
    {
        _currentCanvas = currentCanvas;
        SceneManager.LoadScene(targetScene);
    }
    public void SwitchScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "CutScene":
                if (_currentCanvas == null) return;
                GameObject.Find("CanvasS1").GetComponent<CanvasGroup>().Hide();
                GameObject.Find(_currentCanvas).GetComponent<CanvasGroup>().Show();
                break;
        }
    }
}
