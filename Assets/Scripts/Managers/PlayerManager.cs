using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    float speed; //The Speed of the Player
    float baseSpeed;
    Rigidbody2D rb; //The Rigidbody of the Player
    InteractionManager player_interaction; //The Interaction Code of the Player

    SpriteRenderer sr;

    PathMaker player_path;

    public SpriteRenderer interaction_notice;

    public bool teleporting;

    Vector3 teleportPos;

    public Image frame;

    public bool canAct;
    

    //Initalize Variables at Start
    void Start()
    {
        canAct = true;
        speed = 10.0f;
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player_interaction = GetComponent<InteractionManager>();
        player_path = GetComponent<PathMaker>();
        teleporting = false;
        //frame = GetComponent<Canvas>().GetComponentInChildren<Image>();
        //interaction_notice = GetComponentInChildren<SpriteRenderer>();
    }

    // Every Frame Do the Following
    // 1) Check if you should move
    // 2) Check if you should start/continue/end interacting with something
    void Update()
    {
        if(!canAct){return;}
        movePlayer();
        interactPlayer();
        slowDownWorld();
        rewind();
    }

    public void updateCanAct(bool canAct_){
        if(canAct==canAct_){return;}
        rb.velocity = Vector2.zero;
        canAct = canAct_;
    }



    //Moves the player based on the current buttons held and the speed variable
    private void movePlayer(){
        rb.velocity = new Vector2(
            (InputManager.getPressed("right") ? 1:0) - (InputManager.getPressed("left") ? 1:0),
            (InputManager.getPressed("up") ? 1:0)-(InputManager.getPressed("down") ? 1:0))
            *speed;
    }

    //Runs the interaction code with respects of the player (other things may be able to interact in the future)
    private void interactPlayer(){
        player_interaction.checkInteract(InputManager.getPressedThisFrame("interact"),InputManager.getPressed("interact"));
        if(player_interaction.obj_interact != null){
            interaction_notice.enabled = true;
        }
        else{
            interaction_notice.enabled = false;
        }
    }

    private void rewind(){
        /* New Version of Teleportation
        if(InputManager.getPressedThisFrame("rewind") && !teleporting){
            startTeleport();
        }
        else if(InputManager.getPressedThisFrame("rewind") && teleporting){
            endTeleport();
        }
        */
        // Middle Ground
        if(InputManager.getPressedThisFrame("rewind") && !player_path.getBuildingPath() && !player_path.getTravelingPath()){
            player_path.startTeleport();
            StartCoroutine(Teleport());
        }
        else if(InputManager.getPressedThisFrame("rewind") && player_path.getBuildingPath() && !player_path.getTravelingPath()){
            player_interaction.invulnerable = true;
            player_path.endTeleport();
        }
        else if(InputManager.getPressedThisFrame("rewind") && player_path.getTravelingPath()){
            player_path.interuptPath();
            player_interaction.invulnerable = false;
        }
        else{
            player_interaction.invulnerable = false;
        }
        /* Old Version of Teleportaion
        if(InputManager.getPressed("rewind")){
            teleporting = true;
            player_interaction.invulnerable = true;
            player_path.teleportBack();
        }
        else if(teleporting){
            teleporting = false;
            player_interaction.invulnerable = false;
            player_path.clear();
        }
        */
    }

    private void startTeleport(){
        teleportPos = this.transform.position;
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport(){
        float startTime = 0f;
        float endTime = 5f;
        while(player_path.getBuildingPath() && startTime < endTime){  
            yield return new WaitForFixedUpdate();
            startTime+=Time.fixedDeltaTime;
        }
        if(player_path.getBuildingPath() && !player_path.getTravelingPath()){
            Debug.Log("Force Teleprot");
            player_interaction.invulnerable = true;
            player_path.endTeleport();
        }
    } 

    private void endTeleport(){
        if(!teleporting){return;}
        this.transform.position = teleportPos;
    }


    private void slowDownWorld(){
        if(InputManager.getPressed("slow")){
            frame.enabled = true;
            Time.timeScale = .5f;
            speed = baseSpeed * 2;
        } 
        else{
            frame.enabled = false;
            Time.timeScale = 1f;
            speed = baseSpeed;
        }
    }

}
