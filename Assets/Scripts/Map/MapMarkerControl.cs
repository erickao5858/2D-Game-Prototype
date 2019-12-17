using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MapMarkerControl : MonoBehaviour
{
    public string message;
    public Text target;
    private void OnMouseDown()
    {
        if (target == null) return;
        target.text = message;
    }
}
