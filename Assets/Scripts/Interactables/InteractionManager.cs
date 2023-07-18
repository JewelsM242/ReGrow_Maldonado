using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    bool interacting = false; //Checks to see if something has continuously been interacted with
    public Interactable obj_interact = null; //The object that CAN be interacted with

    public bool invulnerable = false;

    void Awake(){
        obj_interact = null;
    }
    
    public void checkInteract(bool isStarting, bool isContinuing){
        if(isStarting){
            startCurrentInteraction();
        }
        else if(isContinuing && interacting){
            continueCurrentInteraction();
        }
        else if(interacting){
            endCurrentInteraction();
        }
    }

    public Interactable getObj(){
        return obj_interact;
    }


    //Finds Interactables the object has in range
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponentInParent<Interactable>()){
            var all = other.GetComponentsInParent<Interactable>();
            for (int i = 0; i < all.Length; i++){
                if (all[i].getId()=="pair"){
                    obj_interact = all[i];
                    return;
                }
            }
            obj_interact = other.GetComponentInParent<Interactable>();
        }
        /*else if(other.tag=="Death" && !invulnerable){
            Debug.Log("Die");
            //Destroy(this.gameObject);
        }*/
        else{
            obj_interact = null;
        }
    }

    //Finds Interactables the object has left
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponentInParent<Interactable>()){
            endCurrentInteraction();
            obj_interact = null;
        }
        else{
            obj_interact = null;
        }
    }

    //-----------------------------------------------------------
    // These start/coninue/end are probably player specific
    // and may need different inteactions based on different objects
    private void startCurrentInteraction(){
        if(obj_interact == null){return;}
        obj_interact.startTrigger(this);
        interacting = true;
    }

    private void continueCurrentInteraction(){
        if(obj_interact == null){return;}
        obj_interact.trigger(this);
    }

    private void endCurrentInteraction(){
        if(obj_interact == null){return;}
        obj_interact.endTrigger(this);
        interacting=false;
    }
}
