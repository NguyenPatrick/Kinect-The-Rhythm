using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private bool isCharged;
    private bool isDetected;
    public Sprite charged;
    public Sprite notCharged;
    private SpriteRenderer spriteRender;
    public const string chargeName = "Charge(Clone)";

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = charged;
        setCharged();
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

    public void setCharged(){
        spriteRender.sprite = charged;
        isCharged = true;
    }

    public void setNotCharged(){
        spriteRender.sprite = notCharged;
        isCharged = false;
    }

    public bool getIsDetected()
    {
        return this.isDetected;
    }

    public bool getIsCharged()
    {
        return this.isCharged;
    }

}
