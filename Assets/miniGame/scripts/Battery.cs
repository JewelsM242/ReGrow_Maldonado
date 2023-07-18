using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public GameObject outLine;// outpole
    public GameObject inLine;// inpole

    /// <summary>
    /// start powering
    /// </summary>
    public void StartPower()
    {
        if (outLine != null)
        {
            //Debug.Log("start powering");
            outLine.GetComponent<Line>().Power();
        }
    }
}


