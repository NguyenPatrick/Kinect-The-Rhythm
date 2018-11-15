using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUtil : MonoBehaviour {

    private int colliderCount;

    void OnCollisionEnter2D(Collision2D collision)
    {
        colliderCount++;
        Destroy(collision.gameObject);
        if (colliderCount == 3)
        {

        }
    }
}
