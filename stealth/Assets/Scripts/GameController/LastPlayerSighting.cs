using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {

	public Vector3 position=new Vector3(1000f,1000f,1000f);
	public Vector3 resetPosition=new Vector3(1000f,1000f,1000f);
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed=7f;
	public float musicFadeSpeed=1f;

	private AlarmLight alarm; //reference to AlarmLight script
	private Light mainLight;
	private AudioSource panicAudio;
	private AudioSource[] sirens;

	void Awake()
	{
		alarm = GameObject.FindGameObjectWithTag (Tags.alarm).GetComponent<AlarmLight> ();//GameObject.FindGameObjectWithTag (Tag.alarm) is to find the 
																						//alarm game object while the later part is to get the script component
		mainLight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light>();
		panicAudio = transform.FindChild ("secondaryMusic").GetComponent<AudioSource>();
		GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
		sirens= new AudioSource[sirenGameObjects.Length];
		for (int iii=0; iii<sirenGameObjects.Length; iii++) 
		{
			sirens[iii]=sirenGameObjects[iii].GetComponent<AudioSource>();
		}
	}

	void Update()
	{
		SwitchAlarms ();

	}

	void SwitchAlarms()
	{
		float newIntensity;
		if (position != resetPosition) 
		{
			alarm.alarmOn=true; //turn on alarm light
			newIntensity=lightLowIntensity;
			SirenOnOff("ON");
			MusicFading ("YES");
		}
		else
		{ 
			alarm.alarmOn=false; //turn on alarm light
			newIntensity=lightHighIntensity;
			SirenOnOff("OFF");
			MusicFading ("NO");
		}
		mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime); //fade out main light
	}

	void SirenOnOff(string state)
	{
		for (int iii=0; iii<sirens.Length; iii++) 
		{
			if (!sirens[iii].isPlaying && state=="ON") 
			{
				sirens[iii].Play();
			}
			else if(state=="OFF")
			{
				sirens[iii].Stop();
			}
		}
	}
	void MusicFading(string state)
	{
		if (state == "YES") {
			GetComponent<AudioSource>().volume = Mathf.Lerp (GetComponent<AudioSource>().volume, 0f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
		} else {
			GetComponent<AudioSource>().volume = Mathf.Lerp (GetComponent<AudioSource>().volume,0.8f, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
		}
	}
}
