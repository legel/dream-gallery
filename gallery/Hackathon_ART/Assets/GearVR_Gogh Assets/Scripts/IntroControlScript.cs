using UnityEngine;
using System.Collections;

public class IntroControlScript : MonoBehaviour {
	public float playDelay=1f;
	public AudioClip riserClip;
	// Use this for initialization
	void Start () {
		Invoke ("PlayRiser", playDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void PlayRiser(){
		AudioSource.PlayClipAtPoint (riserClip, Vector3.zero);
	}
}
