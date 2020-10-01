using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [FMODUnity.EventRef] public string music = "event:/Music/Music";

    FMOD.Studio.EventInstance musicFv;
    FMOD.Studio.EventInstance alarm;

    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    void Start()
    {
        musicFv = FMODUnity.RuntimeManager.CreateInstance(music);

        musicFv.start();


    }
   

    public void ToGameplay()
    {
        musicFv.setParameterByName("ToGameplay", 1f);
        Debug.Log("Richiamo musica");
    }

    public void LastSeconds()
    
    {
        musicFv.setParameterByName("PitchShift", 1f);
        alarm = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Alarm");
        alarm.start();
        Debug.Log("Richiamo musica last");
    }

    public void End()
    
    {
        musicFv.setParameterByName("End Game", 1f);
  
        Debug.Log("Game ended");
    }

    public void OnNewItemsGrab(Transform transform)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactions/Grab", transform.position);
        // da qui si richiamano i suoni degli oggetti quando vengono grabbati 
    }

    public void OnNewItemsDrop(Transform transform)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Interactions/Drop", transform.position);
        // da qui si richiamano i suoni degli oggetti quando vengono posati
    }
}
