using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SoundPlayer : MonoBehaviour
{
	Dictionary<string,AudioClip> audios;
	// Use this for initialization
	void Start ()
	{
		audios = new Dictionary<string, AudioClip> ();
		string[] files = Directory.GetFiles ("Sounds");
		for (int i = 0; i < files.Length; i++) {
			audios.Add (files [i].Substring (0, files [i].Length - 4), Resources.Load (files [i]) as AudioClip);
		}
		AudioSource.PlayClipAtPoint (audios ["가속"], new Vector3 (0, 0, 0));
	}

	public AudioClip GetAudio(string filename)
	{
		return audios [filename];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

