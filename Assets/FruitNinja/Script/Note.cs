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
    public GameObject ringPrefab;
    private GameObject innerHitBox;
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

    // used to determine if a note is fully in the inner hitZone
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == HitBox.innerHitBoxName)
        {
            innerHitBox = col.gameObject;
            isFullyInHitZone = true;
        }
        if (col.gameObject.name == HitBox.outerHitBoxName)
        {
            isPartiallyInHitZone = true;
        }

        // when the boundary is hit, the note is destroyed and a red error ring is generated
        if (col.gameObject.name == HitBox.boundaryName)
        {
            Game.validCombo = false;
            isFullyInHitZone = false;
            isPartiallyInHitZone = false;

            Destroy(this.gameObject);
            Vector2 position = innerHitBox.GetComponent<Transform>().position;
            Ring ringObject = ((GameObject)Instantiate(ringPrefab, position, ringPrefab.transform.rotation)).GetComponent<Ring>();
            ringObject.createRedRing();
        }
    }
}
