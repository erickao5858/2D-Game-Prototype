using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public AbstractMap map;
    private float zoom = 0;// record nearest whole number
    private bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        zoom = map.Zoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirst) { SetPOIsScale(0f); isFirst = false; }
        // No change
        if (zoom == map.Zoom) return;
        // Get decimal part
        #region zoom change
        zoom = map.Zoom;
        SetPOIsScale(map.Zoom - Math.Truncate(map.Zoom));
        #endregion
    }
    private void SetPOIsScale(double dec)
    {
        GameObject[] POIs = GameObject.FindGameObjectsWithTag("POI");
        float scale;
        if (dec < 0.25f) scale = 1f;
        else if (dec < 0.50f) scale = 0.9f;
        else if (dec < 0.75f) scale = 0.8f;
        else scale = 0.7f;
        Array.ForEach(POIs, POI => POI.transform.localScale = new Vector3(scale, scale, scale));
    }
}
