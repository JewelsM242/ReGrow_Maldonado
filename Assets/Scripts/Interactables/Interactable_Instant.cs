using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Although this is called Fire it should probably be called timer
//The idea of this class is something that needs to be interacted with to make a timer go down
//Once the timer finishes, the object is finished being interacted with.
public class Interactable_Instant : Interactable
{
    public GameObject door;

    void Start(){
        id = "instant";
    }
    public override void startSubTrigger(InteractionManager actor) {
        if(!isActive){return;}
        door.SetActive(!door.activeSelf);
    }


    public override void subTrigger(InteractionManager actor) {
        return;
    }

    public override void endSubTrigger(InteractionManager actor) {
        return;
    }

}
