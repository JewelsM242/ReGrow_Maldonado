using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame_PlayerManager : Microgame_Asset
{

    public Rigidbody2D rb; //The Rigidbody of the Player

    InteractionManager player_interaction; //The Interaction Code of the Player

    float speed;
    // Start is called before the first frame update
    public override void createMe()
    {
        rb = GetComponent<Rigidbody2D>();
        player_interaction = GetComponent<InteractionManager>();
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        interactPlayer();
        endGame();
    }

    
    private void movePlayer(){
        rb.velocity = new Vector2(
            (InputManager.getPressed("right") ? 1:0) - (InputManager.getPressed("left") ? 1:0),
            (InputManager.getPressed("up") ? 1:0)-(InputManager.getPressed("down") ? 1:0))
            *speed;
    }

    //Runs the interaction code with respects of the player (other things may be able to interact in the future)

    private void interactPlayer(){
        player_interaction.checkInteract(InputManager.getPressedThisFrame("interact"),InputManager.getPressed("interact"));
    }

    private void endGame(){
        if(InputManager.getPressed("slow")){
            GameManager.endMicrogame();
        }
    }
}
