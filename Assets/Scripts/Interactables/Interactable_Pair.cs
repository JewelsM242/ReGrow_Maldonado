using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Pair : Interactable
{
    public Interactable car;

    public Interactable cdr;

    public Interactable current;

    void Start(){
        this.isActive = true;
        car.isActive = true;
        cdr.isActive = false;
        current=car;
        id="pair";
    }

    void Update(){
        if(!car.isActive){
            cdr.isActive = true;
            current=cdr;
        }
        else if(!car.isActive && !cdr.isActive){
            this.isActive = false;
            current=null;
        }
    }

    void reset(){
        this.isActive = true;
        car.isActive = true;
        current=car;
        cdr.isActive = false;
    }

    public Interactable getPair(int num){
        if (num == 0){
            return car;
        }
        else{
            return cdr;
        }
    }

    public override void startSubTrigger(InteractionManager actor) {
        current.startTrigger(actor);
    }
    public override void subTrigger(InteractionManager actor) {
        current.trigger(actor);
    }

    public override void endSubTrigger(InteractionManager actor) {
        current.endTrigger(actor);
    }
}
