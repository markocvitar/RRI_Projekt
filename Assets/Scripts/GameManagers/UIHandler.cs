using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIHandler : MonoBehaviour
{

    public void CharacterSelectButton(){
        GameManager.sharedInstance.LoadCharacterSelect();
    }

    public void StartButton(){
        GameManager.sharedInstance.LoadStageOne();
    }

    public void AboutButton(){
        GameManager.sharedInstance.LoadAboutScene();
    }

    public void Volume(float value){
        Debug.Log("Slider value: " + value);
    }

    public void MainMenuButton(){
        GameManager.sharedInstance.LoadMainMenu();
    }


    public void ExitButton(){
        GameManager.sharedInstance.QuitGame();
    }
}
