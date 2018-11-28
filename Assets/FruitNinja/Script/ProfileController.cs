using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileController : MonoBehaviour {
    public static List<string> profiles;

    public List<GameObject> profileGameObjects;

	// Use this for initialization
	void Start () {
        profileGameObjects = new List<GameObject>();
        PopulateProfileList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PopulateProfileList()
    {
        
    }
}
