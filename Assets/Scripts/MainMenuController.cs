using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
  
    public void PlayGame()
    {
        // Gets the button event and converts it into an integer 
        int selectedCharacter =
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.characterIndex = selectedCharacter;
        /*
            We use the class name GameManager to access its static variables/members.
            In this case the static variable is an instance of the GameManager object.
            The GameManager object is internally created. We then use characterIndex()'s
            setter method to have _characterIndex = selectedCharacter.
        */

 

       

        SceneManager.LoadScene("GamePlay");
    }
}
