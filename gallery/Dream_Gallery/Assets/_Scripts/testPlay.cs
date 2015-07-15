//Video playback script

using UnityEngine;
using System.Collections;

public class testPlay : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		MovieTexture movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}