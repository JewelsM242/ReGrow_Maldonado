using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 5;
    float lifeSpan = 5;

    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan-= Time.deltaTime;
        if(lifeSpan<=0){
            Destroy(this.gameObject);
        }
    }

    public float getSpeed(){
        return speed;
    }


}
