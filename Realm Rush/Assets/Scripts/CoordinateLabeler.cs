using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaulColor = Color.white;
    [SerializeField] Color blockColor = Color.gray;

    Vector2Int coordinates;
    TextMeshPro label;
    WayPoint wayPoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        wayPoint = GetComponentInParent<WayPoint>();
        label.enabled = false;
        DisplayCoordinate();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }
        LabelTextColor();
        ToggleCoordinates();
    }

    void LabelTextColor()
    {
        if (wayPoint.IsplayAble) { label.color = defaulColor; }
        else { label.color = blockColor; }       
    }

    void ToggleCoordinates()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinate()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / EditorSnapSettings.gridSize.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / EditorSnapSettings.gridSize.z);
        label.text = coordinates.ToString();
    }

    void UpdateObjectName()
    {
        transform.parent.name = label.text;
    }
}
