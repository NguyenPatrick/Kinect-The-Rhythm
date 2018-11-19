using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HitBox : MonoBehaviour
{
    public const string innerHitZoneName = "Inner";
    public const string outerHitZoneName = "Outer";
    private bool noteIsInner;
    private bool noteIsOuter;
    private GameObject hitBoxObject;
    private Game.Hand type;


    // detects note collisions
    void OnTriggerEnter2D(Collider2D col)
    {
        if(this.gameObject.name == innerHitZoneName){
            noteIsInner = true;
        }
        if (this.gameObject.name == innerHitZoneName)
        {
            noteIsOuter = true;
        }
    }

    // detects note exits
    void OnTriggerExit2D(Collider2D col)
    {
        if (this.gameObject.name == innerHitZoneName)
        {
            noteIsInner = false;
        }
        if (this.gameObject.name == outerHitZoneName)
        {
            noteIsOuter = false;
        }
    }

    public bool getNoteIsInner(){
        return this.noteIsInner;
    }

    public bool getNoteisOuter(){
        return this.noteIsOuter;
    }
}
