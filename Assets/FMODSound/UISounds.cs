using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string mouseOverButton = "event:/UI/MouseOverButton";
    public string mouseClickedButton = "event:/UI/ButtonClicked";

    FMOD.Studio.EventInstance mouseOverSound;
    FMOD.Studio.EventInstance mouseClickedSound;

    // Use this for initialization
    public void Awake()
    {
       mouseOverSound = FMODUnity.RuntimeManager.CreateInstance(mouseOverButton);

       mouseClickedSound = FMODUnity.RuntimeManager.CreateInstance(mouseClickedButton);
    }


    public void PlaymouseOver()
    {
        mouseOverSound.start();
    }

    public void PlaymouseClicked()
    {
        mouseClickedSound.start();
    }
}
