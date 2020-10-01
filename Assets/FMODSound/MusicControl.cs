using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{

	[FMODUnity.EventRef]
	public string music = "event:/Music/Music";

	FMOD.Studio.EventInstance MusicFv;

	// Use this for initialization
	void Start()
	{
		MusicFv = FMODUnity.RuntimeManager.CreateInstance(music);

		MusicFv.start();
	}

	// Con queste funzioni
	public void ToPreparationMusic()
	{

		//MusicFv.setParameterValue("PitchShift", 1f);
		MusicFv.setParameterByName("PitchShift", 0f);
	}


	public void ToBattleMusic()
	{

		//MusicFv.setParameterValue("End Game", 1f);
		MusicFv.setParameterByName("End Game", 0f);
	}
}
