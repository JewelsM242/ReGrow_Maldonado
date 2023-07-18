using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Active_Trigger : Trigger
{
    public Interactable _interactable;

    public bool toggleState;

    public Active_Trigger(Interactable inter, bool tState, string eName)
    {
        eventName = eName;
        _interactable = inter;
        toggleState = tState;
        next = null;
        autoNext = true;
    }


    public override void activate()
    {
        //Debug.Log("Active Trigger Activated on"+_interactable.name);
        _interactable.setActive(toggleState);
    }

    public override void deactivate()
    {
        //_interactable.setActive(!toggleState);
        return;
    }
}
