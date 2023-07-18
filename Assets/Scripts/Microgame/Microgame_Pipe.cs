using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame_Pipe : Microgame_Scene
{
    public LevelManager _levelManager;
    // Start is called before the first frame update
    void Start()
    {
        this.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_levelManager.done){
            Debug.Log("End Game");
            endMicrogame();
        }
    }

    public override void startMicrogame(){
        Debug.Log("Call this one");
        loadMicrogame();
        _levelManager.initialize();

    }

    public override void endMicrogame(){
        Debug.Log("Call this one");
        unloadMicrogame();
        
    }
}
