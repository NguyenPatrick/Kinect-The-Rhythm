using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProfileConcept : MonoBehaviour {

    public static int numberOfUserProfiles;
    public static int currentUserNumber;
    public static string[][] allUserProfiles;


    // user profile data: array of strings
    // user game data biceps: array of strings, seperated by commas
    // user game data shoulders: array of strings, seperated by commas
    public static string[] profileData;
    public static string[][] gameDataBiceps;
    public static string[][] gameDataShoulders;


    public static void addElementToString(string baseString, string stringToBeAdded)
    {
        baseString = baseString + "," + stringToBeAdded;
    }

    public static string[] convertStringToArray(string baseString)
    {
        return baseString.Split(',');
    }

    public static void createNewProfile()
    {
        numberOfUserProfiles = PlayerPrefs.GetInt("numberOfUserProfiles");

        bool validProfile = true;

        // retrieved from onClick()
        int profileId = 1;
        string firstName = "John";
        string lastName = "Cena";
        string leftOrRight = "Both";
        string pictureLocation = "src/src/src";

        string[] profileData = new string[5];
        profileData[1] = firstName;
        profileData[2] = lastName;
        profileData[3] = leftOrRight;
        profileData[4] = pictureLocation; // not mandatory

        // if pictureLocation is not speicified
        // use default unity picture

        // if all mandatory fields are inputted
        if(validProfile)
        {
            profileData[0] = numberOfUserProfiles.ToString();
            PlayerPrefsX.SetStringArray("userProfile" + numberOfUserProfiles, profileData);
            numberOfUserProfiles = numberOfUserProfiles + 1;
            PlayerPrefs.SetInt("numberOfUserProfiles", numberOfUserProfiles);
        }

        ///////////////// only done in the profile page
        // for int i from 0 to numberOfUserProfiles
        // if PlayerPrefsX.GetStringArray("userProfile" + i) != null;
        // add to allUserProfiles
        // load data accordingly

     //  PlayerPrefsX.SetStringArray("userGameData" + numberOfUserProfiles, );




    }
    // Update is called once per frame
    void Update () {
        
    }
}
