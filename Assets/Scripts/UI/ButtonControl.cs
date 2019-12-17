using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonControl : MonoBehaviour
{
    public void StartButton_Click()
    {
        FindCanvasGroup("CanvasS1").Hide();
        FindCanvasGroup("CanvasS2").Show();
    }
    public void SettingButton_Click()
    {
        Debug.Log("Hello World!");
    }
    public void NextButton_Click(int nextNumber)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.Hide();
        FindCanvasGroup("CanvasS" + nextNumber).Show();
    }
    public CanvasGroup FindCanvasGroup(string name)
    {
        return GameObject.Find(name).GetComponent<CanvasGroup>();
    }
}
