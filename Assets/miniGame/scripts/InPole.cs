using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// inPole
/// </summary>
public class InPole : MonoBehaviour
{
    public Battery battery;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            //Debug.Log("connected");
            battery.inLine = other.transform.parent.parent.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            //Debug.Log("severed");
            battery.inLine = null;
        }
    }
}


