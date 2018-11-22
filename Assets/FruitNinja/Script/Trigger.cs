using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    private bool isDetected;
    private bool isTriggered;
    public Sprite trigged;
    public Sprite notTriggered;
    private SpriteRenderer spriteRender;
    public const string triggerName = "Trigger(Clone)";

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = notTriggered;
        setNotTriggered();
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


    public void setTriggered()
    {
        spriteRender.sprite = trigged;
        isTriggered = true;
    }

    public void setNotTriggered()
    {
        spriteRender.sprite = notTriggered;
        isTriggered = false;
    }

    public bool getIsDetected(){
        return this.isDetected;
    }


    public bool getIsTriggered()
    {
        return this.isTriggered;
    }
}
