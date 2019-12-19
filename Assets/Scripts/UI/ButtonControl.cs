using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
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
        GameManager.Instance.SwitchScene(gameObject.transform.parent.name, "RealWorldMap");
    }
    public void BackButton_Click()
    {
        SceneManager.LoadScene("CutScene");
    }
    public void ButtonToLevel1_Click(int subLevel)
    {
        GameManager.Instance.SwitchScene(gameObject.transform.parent.name,"Level1'" + subLevel);
    }
}
