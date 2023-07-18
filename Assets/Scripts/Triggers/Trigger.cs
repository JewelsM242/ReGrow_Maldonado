using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger
{
    public string eventName;

    public Trigger next;

    public bool autoNext;
    /*
    public Trigger()
    {
        Debug.Log("A Base Trigger Should Not be Constructed");
    }
    */

    public void addNext(Trigger n){
        next = n;
    }

    public void startTrigger(){
        activate();
        TriggerManager.makeCurTrigger(this);
    }

    public virtual void activate()
    {
        Debug.Log("Activated");
    }

    public string getEventName()
    {
        return eventName;
    }

    public Trigger endTrigger()
    {
        deactivate();
        return next;
    }

    public virtual void deactivate()
    {
        Debug.Log("Deactivate");
    }

    public string toString(){
        if (next != null){
            return getEventName() +"\n"+ next.toString();
        }else{
            return getEventName();
        }
    }
}
