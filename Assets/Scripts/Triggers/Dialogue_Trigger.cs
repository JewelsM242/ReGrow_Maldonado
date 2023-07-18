using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue_Trigger : Trigger
{
    public TextBoxHandler LocationForText;

    public string DesiredDialogue;

    public Dialogue_Trigger(TextBoxHandler Loc, string desDialogue, string eName)
    {
        eventName = eName;
        LocationForText = Loc;
        DesiredDialogue = desDialogue;
        next = null;
        autoNext = false;
    }


    public override void activate()
    {
        LocationForText.WriteThis(DesiredDialogue, 30);
    }

    public override void deactivate()
    {
        if (next == null){
            LocationForText.FadeAway();
        }
    }
}
