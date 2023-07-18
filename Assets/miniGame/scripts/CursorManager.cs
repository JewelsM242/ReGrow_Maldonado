using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manage the click of the mouse
/// </summary>
public class CursorManager : Singleton<CursorManager>
{
    private Vector3 mouseWorldPos =>
    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //detect the mouse action
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }
    private void ClickAction(GameObject clickObject)
    {
        if (clickObject != null)
        {
            switch (clickObject.tag)
            {
                case "Line":
                    var line = clickObject.GetComponent<Line>();
                    line?.Rotate();
                    Debug.Log(LevelManager.Instance);
                    LevelManager.Instance.StartPowerCommand();
                    break;
            }
        }
    }
    /// <summary>
    /// detect the collider which touches the cursor
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        LayerMask mask = LayerMask.GetMask("SubGame");
        return Physics2D.OverlapPoint(mouseWorldPos, mask);
    }
}

