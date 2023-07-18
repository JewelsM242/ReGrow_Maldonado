using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isActive = true; //if the Interactable can be interacted with right now

    public List<Trigger> triggers;

    public string id;

    public List<SpriteRenderer> activeSprites;
    
    public bool isInteracting = false; //if the Interactable is being Interacted with right now

    public bool activeTogglesCollider = false;

    void Awake()
    {
        triggers = new List<Trigger>();
        toggleActiveSprite(isActive);
        if(activeTogglesCollider){GetComponent<BoxCollider2D>().enabled = isActive;}
    }

    public void toggleActiveSprite(bool toggle){
        for(int i = 0; i<activeSprites.Count;i++){
            activeSprites[i].enabled = toggle;
        }
    }

    public void startTrigger(InteractionManager actor)
    {
        tryTrigger("start");
        Goal_Manager.UpdateInteractable(this,"start");
        startSubTrigger(actor);
    }
    public void trigger(InteractionManager actor)
    {
        tryTrigger("hold");
        subTrigger(actor);
    }

    public void endTrigger(InteractionManager actor)
    {
        tryTrigger("end");
        Goal_Manager.UpdateInteractable(this,"end");
        endSubTrigger(actor);
    }

    public void setActive(bool b)
    {
        if (b) { tryTrigger("on"); Goal_Manager.UpdateInteractable(this,"on");}
        else { tryTrigger("off"); Goal_Manager.UpdateInteractable(this,"off"); }
        isActive = b;
        toggleActiveSprite(isActive);
        if(activeTogglesCollider){GetComponent<BoxCollider2D>().enabled = isActive;}
        
    }

    public virtual void startSubTrigger(InteractionManager actor) {
        Debug.Log("a BaseInteractable was started by " + actor.gameObject.name +
                    ". this should not happen!");
    }
    public virtual void subTrigger(InteractionManager actor) {
        Debug.Log("a BaseInteractable was triggered by " + actor.gameObject.name +
                    ". this should not happen!");
    }

    public virtual void endSubTrigger(InteractionManager actor) {
        Debug.Log("a BaseInteractable was unTriggered by " + actor.gameObject.name +
                    ". this should not happen!");
    }

    private void tryTrigger(string str)
    {
        for(int i=0; i<triggers.Count; i++)
        {
            if (triggers[i].getEventName() == str)
            {
                triggers[i].startTrigger();
                triggers.RemoveAt(i);
                return;
            }
        }
    }

    public void appendToMe(Trigger trigger)
    {
        triggers.Add(trigger);
    }


    public string getId(){
        return id;
    }
}
