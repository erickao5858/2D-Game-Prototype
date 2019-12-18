using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private string _currentCanvas;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchScene(string currentCanvas, string targetScene)
    {
        _currentCanvas = currentCanvas;
        SceneManager.LoadScene(targetScene);
    }
    public void SwitchScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
        GameObject.Find("CanvasS1").GetComponent<CanvasGroup>().Hide();
        GameObject.Find(targetScene).GetComponent<CanvasGroup>().Show();
    }
}
