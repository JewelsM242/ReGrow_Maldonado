using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Line line;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint") && other.transform.parent.parent.CompareTag("Line"))
        {
            //Debug.Log("connected");
            line.AddNextLine(other.transform.parent.parent.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint") && other.transform.parent.parent.CompareTag("Line"))
        {
            //Debug.Log("severed");
            line.DeleteNextLine(other.transform.parent.parent.gameObject);
        }
    }
}
