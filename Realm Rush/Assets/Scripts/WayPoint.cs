using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlayAble;

    private void OnMouseDown()
    {
        if (!isPlayAble) { return; }
        Debug.Log(transform.name);
    }
}
