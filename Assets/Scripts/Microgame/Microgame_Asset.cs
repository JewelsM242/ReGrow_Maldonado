using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame_Asset : MonoBehaviour
{
    public Vector3 originalPosition;
    bool firstCreate = true;

    public void mainCreate()
    {
        if(firstCreate){
            originalPosition = this.transform.position;
            createMe();
            firstCreate = false;
        }
        else{
            this.transform.position = originalPosition;
        }
    }
    public virtual void createMe()
    {
        Debug.Log("BaseMicroGameAsset: Don't call me!");
    }
}
