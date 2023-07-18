using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Although this is called Fire it should probably be called timer
//The idea of this class is something that needs to be interacted with to make a timer go down
//Once the timer finishes, the object is finished being interacted with.
public class Interactable_Timer : Interactable
{
    float curProgress; // current ammount of timer finished
    float progressSpeed; // speed at whihc the timer fills
    
    float degradeSpeed; // speed at whihc the timer empties
    float endProgress; // end ammount of timer to reach

    float min = 0; //if it is 0 the timer cannot impose a bad effect, else it gives a bad effect.

    float resetNum; //what the number resets too after hitting the min;

    bool isDangerous = false;

    float factor;

    Image progressCircle; //visual indicator of progress

    Vector3 progressCircleOffset;

    public GameObject prefabToCopy;

    public GameObject progCircle;

    public void setMin(float num){
        min = num;
    }

    public void setReset(float num){
        resetNum = num;
    }

    // Initialize Sample "Fire"
    public void baseStart(float startProg, float progSped, float degSped, float endProg)
    {
        id = "timer";
        curProgress = startProg;
        progressSpeed = progSped;
        degradeSpeed = degSped;
        endProgress = endProg;
        factor = endProg;
        resetNum = min;
        progressCircleOffset=new Vector3(this.transform.position.x,this.transform.position.y+1.5f,this.transform.position.z);
        createProgressCircle();   
        progressCircle.fillAmount = 0;
    }

    private void createProgressCircle(){
var     progCircle = Instantiate(prefabToCopy,Vector3.zero,Quaternion.identity);
        var canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        progCircle.transform.SetParent(canvas);
        progCircle.transform.localPosition=Vector3.zero;
        progressCircle = progCircle.GetComponentInChildren<Image>(); 
    }

    public void moveProgressCircle(){      
        // Calculate *screen* position (note, not a canvas/recttransform position)
        var canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(progressCircleOffset);
        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPoint, null, out canvasPos);
        progressCircle.rectTransform.localPosition = canvasPos;
    }

    // Every Frame Do the Following
    // 1) Check if active, if not skip the rest
    // 2) If not being interacted with and if curProgress > 0
    //      a) if so decrease the curProgress to a min of 0
    // 3) If curProgress > endProgress mark the intreaction as complete 
    public void baseUpdate()
    {
        moveProgressCircle();
        if(!isActive){return;}
        if(!isInteracting && curProgress > min){
            curProgress -= degradeSpeed * Time.deltaTime; 
            if(curProgress <= min) {
                if(isDangerous){activateDanger();}
                curProgress = resetNum;}
            if(curProgress < 0 && !isDangerous)  {pastZero();}
        }
        if(curProgress >= endProgress){completeInteraction();}
        if(curProgress >= 0 && isDangerous)  {aboveZero();}
        updateTimerState();
    }

    public void updateTimerState(){
        //if(curProgress > min){Debug.Log("Interactable_Fire:"+curProgress+"/"+factor);}
        progressCircle.fillAmount = Mathf.Abs(curProgress)/factor;
    }

    public override void startSubTrigger(InteractionManager actor) {
        if(!isActive){return;}
        isInteracting = true;
    }


    public override void subTrigger(InteractionManager actor) {
        if(isActive == false){return;}
        curProgress += progressSpeed*Time.deltaTime;
    }

    public override void endSubTrigger(InteractionManager actor) {
        isInteracting = false;
        if(isActive == false){return;}
        //isInteracting = false;
    }

    public void completeInteraction(){
        //Debug.Log("Time is Done");
        curProgress = 0;
        setActive(false);
    }

    public void aboveZero(){
        //Debug.Log("Timer is not Dangerous");
        isDangerous = false;
        factor = endProgress;
        progressCircle.GetComponentInChildren<Image>().color = Color.white;
    }

    public void pastZero(){
        //Debug.Log("Timer is Dangerous");
        isDangerous = true;
        factor = Mathf.Abs(min);
        progressCircle.GetComponentInChildren<Image>().color = Color.red;
    }

    public virtual void activateDanger(){
        Debug.Log("Don't run this one");
    }
}
