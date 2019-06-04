using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIEvent : MonoBehaviour
{
    [SerializeField]
    MainMenuUI mainMenu;

    public void AnimFinished()
    {
        mainMenu.Startfade();
    }

}
