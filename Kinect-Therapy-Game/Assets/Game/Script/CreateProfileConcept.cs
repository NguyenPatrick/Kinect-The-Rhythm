using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProfileConcept : MonoBehaviour {
    
    public static int currentUserNumber;
    public static int numberOfUserProfiles;
    public static string[][] allUserProfiles;


    // user profile data: array of strings
    // user game data biceps: array of strings, seperated by commas
    // user game data shoulders: array of strings, seperated by commas
    public static string[] profileData;
    public static string[][] gameDataBiceps;
    public static string[][] gameDataShoulders;


    public static void createNewProfile()
    {
        numberOfUserProfiles = PlayerPrefs.GetInt("numberOfUserProfiles");

        // retrieved from onClick()
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
        if(profileData[1] != null && profileData[2] != null)
        {
            currentUserNumber = numberOfUserProfiles;
            profileData[0] = numberOfUserProfiles.ToString();
            PlayerPrefsX.SetStringArray("userProfile" + currentUserNumber, profileData);
            PlayerPrefsX.SetStringArray("userGameDataBiceps" + currentUserNumber, new string[25]);
            PlayerPrefsX.SetStringArray("userGameDataShoulders" + currentUserNumber, new string[25]);

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



    public static void addElementToString(string baseString, string stringToBeAdded)
    {
       //if(baseString )
        baseString = baseString + "," + stringToBeAdded;
    }

    public static string[] convertStringToArray(string baseString)
    {
        return baseString.Split(',');
    }

    // [0] = total notes
    // [1] = notes missed
    public static void saveDataBiceps(string[] gameData)
    {
        string[] allBicepsData = PlayerPrefsX.GetStringArray("userGameDataBiceps" + currentUserNumber);
        parseGameData(gameData, allBicepsData);

        PlayerPrefsX.SetStringArray("userGameDataBiceps" + currentUserNumber, allBicepsData);
    }

    public static void saveDataShoulders(string[] gameData)
    {
        string[] allShouldersData = PlayerPrefsX.GetStringArray("userGameDataShoulders" + currentUserNumber);
        parseGameData(gameData, allShouldersData);

        PlayerPrefsX.SetStringArray("userGameDataShoulders" + currentUserNumber, allShouldersData);
    }

    private static void parseGameData(string[] gameData, string[] allGameData)
    {
        string gameDataString = "";

        for (int i = 0; i < gameData.Length; i++)
        {
            addElementToString(gameDataString, gameData[i]);
        }

        bool flag = false;

        for (int i = 0; i < allGameData.Length; i++)
        {
            if (allGameData[i] == null)
            {
                flag = true;
                allGameData[i] = gameDataString;
                break;
            }
        }

        if(flag == false)
        {
            allGameData[0] = null;

            for (int i = 1; i < allGameData.Length; i++)
            {
                allGameData[i - 1] = allGameData[i];
            }

            allGameData[allGameData.Length - 1] = gameDataString;
        }
    }
}
