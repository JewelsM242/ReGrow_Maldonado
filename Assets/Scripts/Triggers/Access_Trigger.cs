using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Access_Trigger : Trigger
{
    public string _goal_name;

    public bool toggleState;

    public Access_Trigger(string goal_name, bool tState, string eName)
    {
        eventName = eName;
        _goal_name = goal_name;
        toggleState = tState;
        next = null;
        autoNext = true;
    }


    public override void activate()
    {
        Debug.Log("Access Trigger Activated on goal: "+_goal_name);
        var g = Goal_Manager.getGoalByName(_goal_name);
        if(g==null){
            Debug.Log("FailedToFindGoal: "+_goal_name);
            return;
        }
        Debug.Log(g.getName());
        g.updateUsable(toggleState);
    }

    public override void deactivate()
    {
        //_interactable.setActive(!toggleState);
        return;
    }
}
