using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HitBox : MonoBehaviour
{
    public const string noteName = "Note(Clone)";
    private bool noteIsTouching;
    private GameObject noteObject;


    // detects note collisions
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == noteName)
        {
            noteIsTouching = true;
        }

        noteObject = col.gameObject;
    }

    // detects note exits
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == noteName)
        {
            noteIsTouching = false;
        }

        noteObject = null;
    }

    public GameObject getNoteObject()
    {
        return this.noteObject;
    }

    public bool getNoteIsTouching(){
        return this.noteIsTouching;
    }

}
