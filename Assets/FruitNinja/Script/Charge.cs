using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private bool isCharged;
    private GameObject chargeObject;

    public Charge(GameObject chargeObject){
        this.chargeObject = chargeObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Hand") {
            isCharged = true;
        }
    }

    public GameObject getChargeObject()
    {
        return this.chargeObject;
    }

    public void resetIsCharged()
    {
        this.isCharged = false;
    }

    public bool getIsCharged()
    {
        return this.isCharged;
    }

}
