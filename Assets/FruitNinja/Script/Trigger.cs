using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    private bool isTriggered;
    private GameObject triggerObject;

    public Trigger(GameObject triggerObject){
        this.triggerObject = triggerObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Hand")
        {
            Debug.Log("TRIGGER");

        }
    }

    public GameObject getTriggerObject(){
        return this.triggerObject;
    }

    public void resetIsTriggered()
    {
        this.isTriggered = false;
    }

    public bool getIsTriggered()
    {
        return this.isTriggered;
    }
}
