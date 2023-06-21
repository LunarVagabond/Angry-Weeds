using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void MuteToggle(bool audioOn){
        if(audioOn){
            AudioListener.volume = 1;
        } else{
            AudioListener.volume = 0;
        }
    }
}
