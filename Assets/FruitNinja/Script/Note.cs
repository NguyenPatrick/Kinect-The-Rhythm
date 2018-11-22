using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * IDEAS: Layered Note --> 2 layers, have to hit twice
 * 
*/
public class Note : MonoBehaviour
{

    public bool isPartiallyInHitZone;
    public bool isFullyInHitZone;

    public const string noteName = "Note(Clone)";
    private SpriteRenderer spriteRender;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        isPartiallyInHitZone = false;
        isFullyInHitZone = false;
    }


    // *****                                                                                            
    // WHENEVER NOTE ENTER HITBOX COLLISION, SET isInHitBox TO TRUE
    // WHENEVER THERE IS AS DEQUEUE, MEANING NOTE IS DESTROYED, SET isInHitBox TO FALSE
    // *****


    // if the note is in the hit zone then isInHitZone is set to true
    // accuaracy is defined as if the user hits the the object when it is completely inside the 
    // box hit zone
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == HitBox.innerHitBoxName)
        {
            isFullyInHitZone = true;
        }
        if (col.gameObject.name == HitBox.outerHitBoxName)
        {
            isPartiallyInHitZone = true;
        }
    
        if (col.gameObject.name == HitBox.deleteName)
        {
            Destroy(this.gameObject);
        }

      
    }

    // if the note exits the hitZone, destroy the note and change the animation to red 
    // and make the user lose their combo
    void OnTriggerExit2D(Collider2D col)
    {

        if(col.gameObject.name == HitBox.innerHitBoxName)
        {
            isFullyInHitZone = false;
        }
        if (col.gameObject.name == HitBox.outerHitBoxName)
        {
            isPartiallyInHitZone = false;
        }
    }
}
