using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// outPole
/// </summary>
public class OutPole : MonoBehaviour
{
    public Battery battery;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            //Debug.Log("connected");
            battery.outLine = other.transform.parent.parent.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            //Debug.Log("severed");
            battery.outLine = null;
        }
    }
}

