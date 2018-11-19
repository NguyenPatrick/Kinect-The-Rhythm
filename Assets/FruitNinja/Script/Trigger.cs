using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    private bool isDetected;
    private bool isTriggered;

    void Start()
    {
        isTriggered = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Hand")
        {
            isDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Hand")
        {
            isDetected = false;
        }
    }

    public bool getIsDetected(){
        return this.isDetected;
    }

    public void setIsTriggered(bool val)
    {
        this.isTriggered = val;
    }

    public bool getIsTriggered()
    {
        return this.isTriggered;
    }
}
