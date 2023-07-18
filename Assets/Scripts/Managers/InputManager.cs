using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton;

    public InputAction downAction;
    bool pressedDown;

    public  InputAction upAction;
    bool pressedUp;

    public  InputAction leftAction;
    bool pressedLeft;

    public  InputAction rightAction;
    bool pressedRight;

    public  InputAction interactAction;
    bool pressedInteract;

    public  InputAction slowAction;
    bool pressedSlow;

    public  InputAction rewindAction;
    bool pressedRewind;

    public  InputAction triggerAction;
    bool pressedTrigger;
    // Activates all the availabe Actions at the start of the game
    void Awake()
    {
        Singleton = this;
        Singleton.downAction.Enable();
        Singleton.upAction.Enable();
        Singleton.leftAction.Enable();
        Singleton.rightAction.Enable();
        Singleton.interactAction.Enable();
        Singleton.slowAction.Enable();
        Singleton.rewindAction.Enable();
        Singleton.triggerAction.Enable();
    }

    // Checks the State of Every Action
    void Update()
    {
        //checkPressed();
        //checkPressedThisFrame();
    }

    private void checkPressed(){
        Singleton.pressedDown = Singleton.downAction.IsPressed(); 
        Singleton.pressedUp = Singleton.upAction.IsPressed();   
        Singleton.pressedLeft = Singleton.leftAction.IsPressed();     
        Singleton.pressedRight = Singleton.rightAction.IsPressed();   
        Singleton.pressedInteract = Singleton.interactAction.IsPressed();
        Singleton.pressedSlow = Singleton.slowAction.IsPressed();
        Singleton.pressedRewind = Singleton.rewindAction.IsPressed();
        Singleton.pressedTrigger = Singleton.triggerAction.IsPressed();
    }

    private void checkPressedThisFrame(){
        Singleton.pressedDown = Singleton.downAction.triggered; 
        Singleton.pressedUp = Singleton.upAction.triggered;   
        Singleton.pressedLeft = Singleton.leftAction.triggered;     
        Singleton.pressedRight = Singleton.rightAction.triggered;   
        Singleton.pressedInteract = Singleton.interactAction.triggered;
        Singleton.pressedSlow = Singleton.slowAction.triggered;
        Singleton.pressedRewind = Singleton.rewindAction.triggered;
        Singleton.pressedTrigger = Singleton.triggerAction.triggered;
    }

    //Checks if the desired Action is currently being preformed / was performed this frame
    public static bool getPressed(string keyToCheck, bool thisFrame){
        if( thisFrame ){ Singleton.checkPressedThisFrame(); }
        else{ Singleton.checkPressed(); }
        switch(keyToCheck.ToLower())
        {
            case "down":
                return Singleton.pressedDown;
            case "up":
                return Singleton.pressedUp;
            case "left":
                return Singleton.pressedLeft;
            case "right":
                return Singleton.pressedRight;
            case "interact":
                return Singleton.pressedInteract;
            case "slow":
                return Singleton.pressedSlow;
            case "rewind":
                return Singleton.pressedRewind;
            case "trigger":
                return Singleton.pressedTrigger;
            default:
                Debug.LogError("(InputManager) Get Pressed Called with invalid argument:"+keyToCheck);
                return false;
        }
    }

    public static bool getPressedDirty(string keyToCheck, bool thisFrame){
        switch(keyToCheck.ToLower())
        {
            case "down":
                return Input.GetKey(KeyCode.S);
            case "up":
                return Input.GetKey(KeyCode.W);
            case "left":
                return Input.GetKey(KeyCode.A);
            case "right":
                return Input.GetKey(KeyCode.D);
            case "interact":
                return Input.GetKey(KeyCode.Space);
            case "slow":
                return Input.GetKey(KeyCode.LeftShift);
            case "rewind":
                return Input.GetKey(KeyCode.Q);
            case "trigger":
                return Input.GetKey(KeyCode.Return);
            default:
                Debug.LogError("(InputManager) Get Pressed Called with invalid argument:"+keyToCheck);
                return false;
        }
    }

    public static bool getPressedThisFrameDirty(string keyToCheck, bool thisFrame){
        switch(keyToCheck.ToLower())
        {
            case "down":
                return Input.GetKeyDown(KeyCode.S);
            case "up":
                return Input.GetKeyDown(KeyCode.W);
            case "left":
                return Input.GetKeyDown(KeyCode.A);
            case "right":
                return Input.GetKeyDown(KeyCode.D);
            case "interact":
                return Input.GetKeyDown(KeyCode.Space);
            case "slow":
                return Input.GetKeyDown(KeyCode.LeftShift);
            case "rewind":
                return Input.GetKeyDown(KeyCode.Q);
            case "trigger":
                return Input.GetKeyDown(KeyCode.Return);
            default:
                Debug.LogError("(InputManager) Get Pressed Called with invalid argument:"+keyToCheck);
                return false;
        }
    }

    public static bool getPressed(string keyToCheck){
        return getPressedDirty(keyToCheck,false);
    }

    public static bool getPressedThisFrame(string keyToCheck){
        return getPressedThisFrameDirty(keyToCheck,true);
    }

}
