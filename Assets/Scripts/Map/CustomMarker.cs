using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CustomMarker : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    Vector2d[] _locationVectors;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    [SerializeField]
    Text _targetTextBox;

    List<GameObject> _spawnedObjects;

    [SerializeField]
    Location[] _locations;
    void Start()
    {
        _locationVectors = new Vector2d[_locations.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locations.Length; i++)
        {
            var locationString = _locations[i].Position;
            _locationVectors[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab);
            instance.transform.GetChild(0).GetComponent<TextMesh>().text = _locations[i].Name;// mark text
            if (_targetTextBox != null)
            {
                instance.transform.GetChild(1).GetComponent<MapMarkerControl>().target = _targetTextBox;// specify target textbox
                instance.transform.GetChild(1).GetComponent<MapMarkerControl>().message = _locations[i].Description;// assign message
            }
            instance.transform.localPosition = _map.GeoToWorldPosition(_locationVectors[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }
    }

    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locationVectors[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }
}
[Serializable]
public class Location
{
    public string Name;
    [Geocode]
    public string Position;
    public string Description;
}