using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_End_Microgame : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void startSubTrigger(InteractionManager actor) {
        Debug.Log("End Microgame");
        GameManager.endMicrogame();
    }
    public override void subTrigger(InteractionManager actor) {
        return;
    }

    public override void endSubTrigger(InteractionManager actor) {
        return;
    }
}
