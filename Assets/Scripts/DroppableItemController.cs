using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItemController : MonoBehaviour
{
    public DroppableItemTypes Type;
    public int AddPercent { get; set; }

    public void ResetPosition(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }
}
