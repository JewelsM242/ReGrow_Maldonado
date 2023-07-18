using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    List<Interactable> objectives;
    List<string> activators;
    Trigger rewardTrigger;
    bool isUsable;
    string _name;

    public Goal(List<Interactable> objs, List<string> act, Trigger t){
        objectives = objs;
        activators = act;
        rewardTrigger = t;
        isUsable = true;
        _name = "defaultGoal";
    }

    public Goal(){
        objectives = new List<Interactable>();
        activators = new List<string>();
        rewardTrigger = null;
         isUsable = true;
        _name = "defaultGoal";
    }

    public void addObjective(Interactable obj, string act){
        objectives.Add(obj);
        activators.Add(act); 
    }

    public void addGoalDetails(bool usable, string n){
        isUsable = usable;
        _name = n;
    }

    public void updateUsable(bool t){
        isUsable = t;
        Debug.Log(this.toString());
    }

    public bool getIsUsable(){
        return isUsable;
    }

    public string getName(){
        return _name;
    }

    public int hasObjective(Interactable obj, string act){
        for (int i = 0; i < objectives.Count; i++){
            if (obj == objectives[i] && act == activators[i]){
                return i;
            }
        }
        return -1;
    }

    public bool removeObj(Interactable obj, string act){
        var i = hasObjective(obj,act);
        if(i!=-1){
            objectives.RemoveAt(i);
            activators.RemoveAt(i);
            return true;
        }
        return false;
    }

    public bool checkGoal(){
        return objectives.Count == 0;
    }

    public void addTrigger(Trigger t){
        rewardTrigger = t;
    }

    public void activateTrigger(){
        rewardTrigger.startTrigger();
    }

    public string toString(){
        string str = "-----"+_name+"-----\n";
        str += "Active: "+isUsable+"\n";
        for (int i = 0; i < objectives.Count; i++){
            str += objectives[i].name +" | "+activators[i]+"\n";
        }
        str += rewardTrigger.toString();
        return str;
    }

}
