using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

    public const string ringName = "Ring(Clone)";
    private const float ringDuration = 0.30f;
    public Sprite greenRing;
    public Sprite yellowRing;
    public Sprite redRing;

    public void createGreenRing()
    {
        Destroy(this.gameObject, ringDuration);
        GetComponent<SpriteRenderer>().sprite = greenRing;   
    }

    public void createYellowRing()
    {
        Destroy(this.gameObject, ringDuration);
        GetComponent<SpriteRenderer>().sprite = yellowRing;
    }

    public void createRedRing()
    {
        Destroy(this.gameObject, ringDuration);
        GetComponent<SpriteRenderer>().sprite = redRing;
    }

}
