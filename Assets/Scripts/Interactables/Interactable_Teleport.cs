using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Teleport : Interactable
{
    public GameObject endLocation;
    // Start is called before the first frame update
    void Start()
    {
        id = "teleport";
    }

    public override void startSubTrigger(InteractionManager actor) {
        actor.GetComponent<Rigidbody2D>().position = endLocation.transform.position;
    }
    public override void subTrigger(InteractionManager actor) {
        return;
    }

    public override void endSubTrigger(InteractionManager actor) {
        return;
    }
}
