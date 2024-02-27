using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    Vector2Int coordinates;
    TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        DisplayCoordinate();
        UpdateObjectName();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
        }
    }

    void DisplayCoordinate()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / EditorSnapSettings.gridSize.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / EditorSnapSettings.gridSize.z);
        textMeshPro.text = coordinates.ToString();
    }

    void UpdateObjectName()
    {
        transform.parent.name = textMeshPro.text;
    }
}
