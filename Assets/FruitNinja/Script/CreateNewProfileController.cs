using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class CreateNewProfileController : MonoBehaviour {

    public InputField firstNameInput;
    public InputField lastNameInput;
    public Dropdown gameTypeDropdown;
    public Button createProfileButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClickCreateProfileButton()
    {
<<<<<<< Updated upstream
        //ProfileController.profiles.Add();
=======
        string profileString = lastNameInput.text + " " + firstNameInput.text + " " + gameTypeDropdown.itemText;
        ProfileController.profiles.Add(profileString);
        ProfileController.profiles.Sort();
>>>>>>> Stashed changes
    }

}
