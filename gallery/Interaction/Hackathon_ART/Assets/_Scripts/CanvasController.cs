using UnityEngine;
using System.Collections;

public class CanvasController : MonoBehaviour {

	public GameObject player;
	public GameObject[] canvases;
	public float distanceFromCenter;
	private GameObject firstCanvas;



	void Awake(){

		player = GameObject.FindGameObjectWithTag("Player");
		firstCanvas = GameObject.FindGameObjectWithTag("refrencePoint");
		distanceFromCenter = player.transform.position.z - firstCanvas.transform.position.z;

	}
	void setCanvas(){
		for(int i = 0; i < canvases.Length; i++){
			Instantiate(canvases[i]);

				

		}



	}

}
