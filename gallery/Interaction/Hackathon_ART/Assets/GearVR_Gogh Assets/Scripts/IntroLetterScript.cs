using UnityEngine;
using System.Collections;

public class IntroLetterScript : MonoBehaviour {
	
	bool hasTriggeredAudio;
	Transform myTransform;
	public AudioClip wooshSound;
	public float myMinimum=.5f;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasTriggeredAudio&&myTransform.localScale.x>myMinimum) {
			AudioSource.PlayClipAtPoint(wooshSound,myTransform.position);
			hasTriggeredAudio=true;
		}
	}
}
