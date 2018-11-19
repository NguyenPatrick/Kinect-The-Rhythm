using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private bool isCharged;
    private bool isDetected;

    private void Start()
    {
        isCharged = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Hand") {
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

    public bool getIsDetected()
    {
        return this.isDetected;
    }

    public void setIsCharged(bool val)
    {
        this.isCharged = val;
    }

    public bool getIsCharged()
    {
        return this.isCharged;
    }

}
