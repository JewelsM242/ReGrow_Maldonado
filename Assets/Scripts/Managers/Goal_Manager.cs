using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Goal_Manager : MonoBehaviour
{
    public List<Goal> allGoals;
    public static Goal_Manager singleton;
    public List<Interactable> testInters;
    public List<string> testString;
    //public string allGoalsTextFile;
    //public Asset textDoc;
    public string file_path;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        /*
        singleton.allGoals = new List<Goal>();
        var hold = TriggerManager.makeMainDialgoueTrigger("Wow you got the fires!", "on");
        var hold2 = TriggerManager.makeMainDialgoueTrigger("Good job", "on");
        hold.addNext(hold2);
        makeGoal(testInters, testString, hold);
        */
        file_path = Path.Combine(Application.streamingAssetsPath, "DesertGoals.txt");
        singleton.allGoals = getAllGoals(file_path);
        printAllGoals();
    }

    public static void printAllGoals(){
        foreach (var goal in singleton.allGoals){
            Debug.Log(goal.toString());
        }
    }

    public static Goal getGoalByName(string gName){
        foreach (var goal in singleton.allGoals){
            Debug.Log(gName+"=="+goal.getName());
            if (goal.getName().Equals(gName)){
                return goal;
            }
        }
        return null;
    }

    // Update is called once per frame
    private static bool UpdateGoal(Goal g)
    {
        if (g.checkGoal()){
            g.activateTrigger();
            singleton.allGoals.Remove(g);
            Debug.Log("Goal Complete: "+g.getName());
            return true;
        }
        return false;
    }   

    public static void UpdateInteractable(Interactable obj, string howItUpdated){
        bool runAgain = false;
        for(int i = 0; i < singleton.allGoals.Count; i++){
            if(singleton.allGoals[i].getIsUsable()){
                if(singleton.allGoals[i].removeObj(obj, howItUpdated)){
                    if (UpdateGoal(singleton.allGoals[i])){
                        runAgain = true;
                        break;
                    }
                }
            }
        }
        if(runAgain){UpdateInteractable(obj,howItUpdated);}
    }

    public static Goal makeGoal(List<Interactable> iList, List<string> sList, Trigger t){
        List<Interactable> hold = iList;
        List<string> hold2 = sList;
        return (new Goal(hold, hold2, t));
    }

    public static Goal makeEmptyGoal(){
        return (new Goal());
    }

    public static Interactable Search(string type, string Name){
        if(type == "Interactable"){
            var maybePair = Name.Split('-');
            string choose = "bad";
            if(maybePair.Length > 1){
                Name = maybePair[0].Trim();
                choose = maybePair[1].Trim();
                var interactables = FindObjectsOfType<Interactable_Pair>();
                foreach (var inter in interactables){
                    if (inter.name == Name){
                        int chooseInt;
                        int.TryParse(choose, out chooseInt);
                        return inter.getPair(chooseInt);
                    }
                }
            }
            else{
                var interactables = FindObjectsOfType<Interactable>();
                foreach (Interactable inter in interactables){
                    if (inter.name == Name){
                        return inter;
                    }
                }
            }
        }
        return null;
    }

    public static List<Goal> getAllGoals(string file_path){
        StreamReader inp_stm = new StreamReader(file_path);
        Queue<Goal> quene = new Queue<Goal>();
        List<Goal> finishedGoals = new List<Goal>();
        Goal curGoal = null;
        Trigger lastTrigger = null;
        Trigger firstTrigger = null;
        int mode = 0; //1 For Objective | 2 For Trigger

        while(!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine( );
            //Debug.Log(inp_ln);
            if(inp_ln == ""){
                continue;
            }
            else if(inp_ln[0] == '#'){
                continue;
            }
            else if(inp_ln == "{Goal}"){
                if (curGoal != null){quene.Enqueue(curGoal);}
                curGoal = makeEmptyGoal();
                mode = 0;
            }
            else if(inp_ln == "{Objective}"){
                mode = 1;
            }
            else if(inp_ln == "{Trigger}"){
                mode = 2;
            }
            else if(inp_ln == "{End Goal}"){
                curGoal.addTrigger(firstTrigger);
                firstTrigger = null;
                lastTrigger = null;
                if(quene.Count == 0){
                    finishedGoals.Add(curGoal);
                    curGoal = null;
                }else{
                    curGoal = quene.Dequeue();
                }
                mode = -1;
            }
            else{
                switch (mode)
                {
                    case 0:
                        var splitLine0 = inp_ln.Split('|');
                        var b = splitLine0[0].Trim();
                        var n = splitLine0[1].Trim();
                        var bt = true;
                        if(b == "false"){
                            bt = false;
                        }
                        curGoal.addGoalDetails(bt,n);
                        break;

                    case 1:
                        var splitLine = inp_ln.Split('|');
                        var inter = splitLine[0].Trim();
                        var act = splitLine[1].Trim();
                        Interactable hold = Search("Interactable", inter);
                        curGoal.addObjective(hold,act);
                        break;

                    case 2:
                        var splitLine2 = inp_ln.Split('|');
                        var type = splitLine2[0].Trim();
                        var data = splitLine2[1].Trim();
                        Trigger t = null;
                        bool i = false;
                        string[] split_data;
                        switch (type)
                        {
                            case "Dialogue":
                                t = TriggerManager.makeMainDialgoueTrigger(data, type+" : "+data);
                                break;

                            case "Activate":
                                split_data = data.Split(",");
                                if(split_data[1].Trim() == "true"){i=true;}
                                Interactable h = Search("Interactable", split_data[0].Trim());
                                t = TriggerManager.makeActiveTrigger(h, i, type+" : "+data);
                                break;

                            case "Access":
                                split_data = data.Split(",");
                                if(split_data[1].Trim() == "true"){i=true;}
                                t = TriggerManager.makeAccessTrigger(split_data[0].Trim(), i, type+" : "+data);
                                break;


                            default:
                                Debug.Log("No Type Given for Trigger");
                                break;
                        }
                        
                        if (lastTrigger != null){
                            lastTrigger.addNext(t);
                            lastTrigger = t;
                        } else {
                            firstTrigger = t;
                            lastTrigger = t;
                        }
                        break;

                    default:
                        Debug.Log("Reading in a 0 case");
                        break;
                }
            }

        }

        inp_stm.Close( );
        return finishedGoals;
    }
}
