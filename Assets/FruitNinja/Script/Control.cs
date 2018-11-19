using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to the trigger and charge gameobjects
public class Control : MonoBehaviour {

    private Trigger trigger;
    private Charge charge;
    private HitBox[] hitBoxes;
    public Sprite activeState;
    public Sprite inActiveState;
    private Game.Hand type;

    public Control(GameObject trigger, GameObject charge, GameObject[] hitBoxes, Game.Hand type){
        this.trigger = trigger.GetComponent<Trigger>();
        this.charge = charge.GetComponent<Charge>();
    }

    public void checkState(){

      
    }

    void Update(){

    }

  
}
