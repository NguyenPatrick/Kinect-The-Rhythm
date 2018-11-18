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

    public bool isPartiallyInHitZone; // some of the note is in the hit zone, half points, no combo
    public bool isFullyInHitZone; // all the note is in the hit zone, full points, combo
    public bool isHitByHand; // if the hand is also in contact with the fruit

    private int spawnPosition; // spawn position of note
    private GameObject noteObject; // prefab of the note
    public const string prefabName = "Note(Clone)";

    public Note(GameObject noteObject, int spawnPosition)
    {
        this.noteObject = noteObject;
        this.spawnPosition = spawnPosition;
    }


    void Update()
    {
        if (isPartiallyInHitZone == false && isFullyInHitZone == true)
        {
            // full points
            //Destroy(gameObject);
        }
        else if (isPartiallyInHitZone == true && isFullyInHitZone == true)
        {
            // partial points
        }
        else
        {
            // no points
        }
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
        if (col.gameObject.name == HitBox.innerHitZoneName)
        {
            isFullyInHitZone = true;
        }
        if (col.gameObject.name == HitBox.outerHitZoneName)
        {
            isPartiallyInHitZone = true;
        }

    }

    // if the note exits the hitZone, destroy the note and change the animation to red 
    // and make the user lose their combo
    void OnTriggerExit2D(Collider2D col)
    {

        if(col.gameObject.name == HitBox.innerHitZoneName)
        {
            isFullyInHitZone = false;
        }
        if(col.gameObject.name == HitBox.outerHitZoneName){
            isPartiallyInHitZone = false;
            Game.removeNote();
        }
    }

    public GameObject getNoteObject(){
        return this.noteObject;
    }

    public int getSpawnPosition()
    {
        return this.spawnPosition;
    }
}
