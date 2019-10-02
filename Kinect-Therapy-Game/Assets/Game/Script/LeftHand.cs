using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
