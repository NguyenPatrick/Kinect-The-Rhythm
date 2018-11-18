using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to the trigger and charge gameobjects
public class Control : MonoBehaviour {

    private Trigger trigger;
    private Charge charge;

    public Sprite activeState;
    public Sprite inActiveState;
    private Game.Hand type;

    public Control(GameObject trigger, GameObject charge, Game.Hand type){
        this.trigger = trigger.GetComponent<Trigger>();
        this.charge = charge.GetComponent<Charge>();
    }

    public void checkState(){

        if (charge.getIsCharged() == true)
        {
            Debug.Log("CHARGEEEEEEEEEE");
            // change sprite from CHARGE! to tinted
            // change sprite of Trigger from NEED CHARGE to READY
        }


        if (trigger.getIsTriggered() == true)
        {
            // change from green to tinted
            Debug.Log("TRIGGEREDDDDD");
        }
    }

    void Update(){

    }

  
}
