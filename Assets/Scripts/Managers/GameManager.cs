using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Microgame_Scene currentSubScene;

    Interactable_Microgame starter;

    public static GameManager singleton;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        singleton.currentSubScene = null;
    }

    void Update(){
        if(TriggerManager.getCurTrigger()!=null){
            singleton.playerEnabled(false);
            //Time.timeScale = 0f;
            Debug.Log("Has Trigger Loaded");
        }
        else{
            if(singleton.currentSubScene != null){
                singleton.playerEnabled(false);
                Time.timeScale = 1f;
                Debug.Log("Has Subscene Loaded");
            }
            else{
                singleton.playerEnabled(true);
                Time.timeScale = 1f;
                Debug.Log("Has Nothing Loaded");
            }
        }
    }

    public static void startMicrogame(Interactable_Microgame interactable, Microgame_Scene microgame)
    {
        singleton.starter = interactable;
        singleton.currentSubScene = microgame;
        microgame.startMicrogame();
        singleton.playerEnabled(false);
    }

    public static void endMicrogame(){
        singleton.starter.setActive(false);
        singleton.currentSubScene.endMicrogame();
        singleton.playerEnabled(true);
        singleton.currentSubScene = null;
    }

    private void playerEnabled(bool state){
        FindObjectOfType<PlayerManager>().updateCanAct(state);
    }

}
