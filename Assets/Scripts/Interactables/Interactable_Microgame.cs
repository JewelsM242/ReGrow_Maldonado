using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Microgame : Interactable
{
    public Microgame_Scene microgame_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void startSubTrigger(InteractionManager actor) {
        if(!isActive){return;}
        Debug.Log("Start Microgame");
        GameManager.startMicrogame(this,microgame_);
    }
    public override void subTrigger(InteractionManager actor) {
        return;
    }

    public override void endSubTrigger(InteractionManager actor) {
        Debug.Log("Leave Microgame");
    }
}
