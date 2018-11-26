using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HitBox : MonoBehaviour
{
    public GameObject ringPrefab;
    private GameObject ringObject;
    private GameObject noteObject;
    private bool noteIsTouching;
    public const string innerHitBoxName = "HitBoxInner(Clone)";
    public const string outerHitBoxName = "HitBoxOuter(Clone)";
    public const string boundaryName = "Boundary(Clone)";

    void Start()
    {
        if(this.gameObject.name == innerHitBoxName)
        {
            Vector2 position = this.GetComponent<Transform>().position;
            this.ringObject = (GameObject)Instantiate(ringPrefab, position, ringPrefab.transform.rotation);
        }
    }

    // detects note collisions
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == Note.noteName)
        {
            noteIsTouching = true;
        }

        noteObject = col.gameObject;
    }

    // detects note exits
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == Note.noteName)
        {
            noteIsTouching = false;
        }

        noteObject = null;
    }

    public GameObject getNoteObject()
    {
        return this.noteObject;
    }

    public GameObject getRingObject()
    {
        return this.ringObject;
    }

    public bool getNoteIsTouching(){
        return this.noteIsTouching;
    }

}
