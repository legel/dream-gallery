using UnityEngine;
using System.Collections;

public class IntroPieceScript : MonoBehaviour {

	bool hasTriggeredAudio;
	Transform myTransform;
	public AudioClip wooshSound;
	public float myMinimum;
	// Use this for initialization
	void Start () {
		myTransform = transform;
		myMinimum = Random.Range (.1f, .8f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasTriggeredAudio&&myTransform.localScale.x>myMinimum) {
			AudioSource.PlayClipAtPoint(wooshSound,myTransform.position);
			hasTriggeredAudio=true;
				}
	}
}
