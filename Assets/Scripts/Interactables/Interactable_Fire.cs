using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Although this is called Fire it should probably be called timer
//The idea of this class is something that needs to be interacted with to make a timer go down
//Once the timer finishes, the object is finished being interacted with.
public class Interactable_Fire : Interactable_Timer
{

    public Projectile fireball;

    // Initialize Sample "Fire"
    void Start()
    {
        baseStart(1, 1, .25f, 5);
        id = "fire";
        setMin(-1);
        setReset(-.5f);
    }   
    

    void Update()
    {
        baseUpdate();
    }

    public override void activateDanger(){
        //shootFire();
    }

    public void shootFire(){
        var playerPos = FindObjectOfType<PlayerManager>().transform.position;
        Debug.Log(playerPos);
        var fireball_copy = Instantiate(fireball,this.transform.position,Quaternion.identity);
        var speed=fireball.getSpeed();
        var dir = -Vector3.Normalize(transform.position-playerPos);
        fireball_copy.GetComponent<Rigidbody2D>().velocity=dir*speed;
    }

}
