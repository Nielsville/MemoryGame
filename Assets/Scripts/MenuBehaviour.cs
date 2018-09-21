using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour{
    public void triggerMenuBehavior(int i){
        switch (i) {
            default:
            case(0):
                SceneManager.LoadScene("MenuAmountCards");
                break;
            case(1):
                Application.Quit();
                break;
            case (2):
                SceneManager.LoadScene("Level1");
                break;
            case (3):
                SceneManager.LoadScene("Level2");
                break;
            case (4):
                SceneManager.LoadScene("Level3");
                break;
        }
    }

}
