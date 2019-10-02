﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class CreateNewProfileController : MonoBehaviour
{
    public InputField firstNameInput;
    public InputField lastNameInput;
    public Dropdown gameTypeDropdown;
    public Button createProfileButton;

    public static int currentUserNumber;
    public static int numberOfUserProfiles;
    public static string[][] allUserProfiles;

    // user profile data: array of strings
    // user game data biceps: array of strings, seperated by commas
    // user game data shoulders: array of strings, seperated by commas
    public static string[] profileData;
    public static string[][] gameDataBiceps;
    public static string[][] gameDataShoulders;

    public static int getNumberOfProfiles()
    {
        return PlayerPrefs.GetInt("numberOfUserProfiles");
    }

    public static string getUserProfile(int id)
    {
        return PlayerPrefs.GetString("userProfile" + id);
    }

    public static int getHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public static void saveHighScore(int newScore)
    {
        if(PlayerPrefs.GetInt("highScore") < newScore)
        {
            PlayerPrefs.SetInt("highScore", newScore);
        }
    }


    public static string arrayToString(string[] array)
    {
        string newString = "";

        for (int i = 0; i < array.Length; i++)
        {
            if (i == array.Length - 1)
            {
                newString = newString + array[i];
            }
            else
            {
                newString = newString + array[i] + ",";
            }
        }

        return newString;
    }


    public void onClickCreateProfileButton()
    {

        numberOfUserProfiles = getNumberOfProfiles();

        string firstName = firstNameInput.text;
        string lastName = lastNameInput.text;
        string pictureLocation = CanvasSampleOpenFileImage.path;

        string[] profileData = new string[4];
        profileData[1] = firstName;
        profileData[2] = lastName;
        profileData[3] = CanvasSampleOpenFileImage.path; // not mandatory

        if (profileData[1] != "" && profileData[2] != "")
        {
            currentUserNumber = numberOfUserProfiles;
            profileData[0] = numberOfUserProfiles.ToString();
            PlayerPrefs.SetString("userProfile" + currentUserNumber, arrayToString(profileData));
            numberOfUserProfiles = numberOfUserProfiles + 1;
            PlayerPrefs.SetInt("numberOfUserProfiles", numberOfUserProfiles);
        }

        Debug.Log(PlayerPrefs.GetString("userProfile" + currentUserNumber));
        PlayerPrefs.DeleteAll();

    }
}
