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
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        DisplayCoordinate();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }
        SetLabelColor();
        ToggleCoordinates();
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else { label.color = defaulColor; }
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
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.ToString();
    }

    void UpdateObjectName()
    {
        transform.parent.name = label.text;
    }
}
