using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame_AmongUs : Microgame_Scene
{

    // Start is called before the first frame update
    void Start()
    {
        this.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void startMicrogame(){
        Debug.Log("Call this one");
        loadMicrogame();

    }

    public override void endMicrogame(){
        Debug.Log("Call this one");
        unloadMicrogame();
        
    }
}
