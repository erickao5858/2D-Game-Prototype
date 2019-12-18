using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonControl : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void SettingButton_Click()
    {
        Debug.Log("Hello World!");
    }
    public void NextButton_Click(Canvas target)
    {
        gameObject.transform.parent.GetComponent<CanvasGroup>().Hide(); // hide current canvas
        target.GetComponent<CanvasGroup>().Show(); // show next canvas
    }
    public void MapButton_Click()
    {
        _gameManager.SwitchScene(gameObject.transform.parent.name, "RealWorldMap");
    }
    public void BackButton_Click()
    {
        _gameManager.SwitchScene("CutScene");
    }
}
