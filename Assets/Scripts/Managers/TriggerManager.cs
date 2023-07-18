using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerManager : MonoBehaviour
{
    public Dictionary<Trigger,Interactable> allTriggers;
    public static TriggerManager singleton;

    public TextBoxHandler tbh;
    
    public TMP_Text mainTextBox;

    public Interactable testInter;

    public Trigger curTrigger;

    void Awake()
    {
        singleton = this;
        singleton.allTriggers = new Dictionary<Trigger, Interactable>();
        singleton.curTrigger = null;
    }

    void Start(){
        //var hold = new Dialogue_Trigger(mainTextBox, "Welcome Player", "off");
        //var hold2 = new Dialogue_Trigger (mainTextBox, "This is a button", "off");
        //var hold3 = new Dialogue_Trigger (mainTextBox, "This is STILL a button", "off");
        //hold2.addNext(hold3);
        //hold.addNext(hold2);
        //attachTrigger(hold, testInter);
    }

    void Update(){
        if(InputManager.getPressedThisFrame("trigger")){
            if (tbh.IsWritingDone()){
                Debug.Log("Go next");
                goNextTrigger();
            }
            else{
                tbh.FinishWriting();
            }
        }
        else{
            Debug.Log("No Trigger");
        }
    }

    public static Trigger getCurTrigger(){
        return singleton.curTrigger;
    }

    public static void makeCurTrigger(Trigger cur){
        singleton.curTrigger = cur;
        if(singleton.curTrigger!=null){
            singleton.curTrigger.activate();
            if(cur.autoNext && cur.next!=null){
                makeCurTrigger(cur.next);
            }
        }
    }

    public static void goNextTrigger(){
        if(singleton.curTrigger==null){Debug.Log("FUCK");return;}
        makeCurTrigger(singleton.curTrigger.endTrigger());
    }

    public static Trigger makeMainDialgoueTrigger(string str, string eName)
    {
        var hold = new Dialogue_Trigger(singleton.tbh, str, eName);
        return hold;
    }

    public static Trigger makeActiveTrigger(Interactable inter, bool state, string eName)
    {
        var hold = new Active_Trigger(inter, state, eName);
        return hold;
    }

    public static Trigger makeAccessTrigger(string goal_name, bool state, string eName)
    {
        var hold = new Access_Trigger(goal_name, state, eName);
        return hold;
    }

    private static void attachTrigger(Trigger trigger, Interactable obj)
    {
        obj.appendToMe(trigger);
        singleton.allTriggers[trigger] = obj;
    }
}
