﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour {
    public static List<string> profiles;
    public Canvas canvas;
    public GameObject ProfileButton;

    public Text ProfilesPageCounter;

    public List<GameObject> profileButtons;

    int lastPageNum;

	// Use this for initialization
	void Start () {
        profiles = new List<string>();
        profileButtons = new List<GameObject>();


        for (int i = 0; i < CreateNewProfileController.getNumberOfProfiles(); i++)
        {
            string profileInfo = CreateNewProfileController.getUserProfile(i);

            if (profileInfo != null)
            {
                profiles.Add(profileInfo);
            }
        }

        profiles.Add("Hello World file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("Bocar Raspberry file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("Kinect Boi file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("three person file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("pi Raspberry file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("one two file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");

        profiles.Add("page two file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");
        profiles.Add("page twotwo file:///Users/nguyenpatrick/Downloads/46501136_1830320980399189_6584738988563628032_o.jpg");


        PopulateProfileButtonsList();

        lastPageNum = (int)Math.Ceiling(profileButtons.Capacity / 6.0);

        DisplayProfiles(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PopulateProfileButtonsList()
    {
        for (int i = 0; i < profiles.Capacity; i++) {
            int coordY = 0;
            switch (i % 6)
            {
                case 0:
                    coordY = 300;
                    break;
                case 1:
                    coordY = 150;
                    break;
                case 2:
                    coordY = 0;
                    break;
                case 3:
                    coordY = -150;
                    break;
                case 4:
                    coordY = -300;
                    break;
                case 5:
                    coordY = -450;
                    break;
            }


            string[] profileStrArray = profiles[i].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            GameObject newButton = Instantiate(ProfileButton, new Vector3(250, 300, 0), Quaternion.identity);
            newButton.transform.SetParent(canvas.transform, false);

            newButton.SetActive(false);
            newButton.GetComponent<RectTransform>().localPosition = new Vector2(0, coordY);
            newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            newButton.GetComponentInChildren<Text>().text = profileStrArray[0] + ", " + profileStrArray[1];

            Debug.Log(profileStrArray[2]);
            StartCoroutine(CanvasSampleOpenFileImage.OutputRoutine(newButton.transform.Find("ProfileImage").GetComponent<RawImage>(), profileStrArray[2]));
            profileButtons.Add(newButton);
        }
    }

    void DisplayProfiles(Boolean visible)
    {
        int pageNum = Int32.Parse(ProfilesPageCounter.text);

        if (profileButtons.Capacity % 6 != 0 &&  lastPageNum == pageNum )
        {
            for (int i = (pageNum - 1) * 6; i < profileButtons.Capacity; i++)
            {
                profileButtons[i].SetActive(visible);
            }
        }
        else
        {
            for (int i = (pageNum - 1) * 6; i < ((pageNum - 1) * 6) + 6; i++)
            {
                profileButtons[i].SetActive(visible);
            }
        }
    }

    public void onNextPageButtonClick()
    {
        int pageNum = Int32.Parse(ProfilesPageCounter.text);

        if (pageNum != lastPageNum)
        {
            DisplayProfiles(false);
            pageNum++;

            ProfilesPageCounter.text = pageNum.ToString();
            DisplayProfiles(true);
        }
    }

    public void onPrevPageButtonClick()
    {
        int pageNum = Int32.Parse(ProfilesPageCounter.text);

        if(pageNum != 1)
        {
            DisplayProfiles(false);
            pageNum--;

            ProfilesPageCounter.text = pageNum.ToString();
            DisplayProfiles(true);
        }
    }
}
