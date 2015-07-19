using UnityEngine;
using System.Collections;

public class FPSScript : MonoBehaviour {
	TextMesh myTextMesh;
	
	// FPS 
	private float  UpdateInterval 	= 0.5f;
	private float  Accum   			= 0; 	
	private int    Frames  			= 0; 	
	private float  TimeLeft			= 0; 				
	private string strFPS			= "FPS: 0";

	// Use this for initialization
	void Start () {
		myTextMesh = GetComponent<TextMesh> ();
	}

	/// <summary>
	/// Updates the FPS.
	/// </summary>
	void Update()
	{
		TimeLeft -= Time.deltaTime;
		Accum += Time.timeScale/Time.deltaTime;
		++Frames;
		
		// Interval ended - update GUI text and start new interval
		if( TimeLeft <= 0.0 )
		{
			// display two fractional digits (f2 format)
			float fps = Accum / Frames;

				myTextMesh.text = System.String.Format("FPS: {0:F2}",fps);
			
			TimeLeft += UpdateInterval;
			Accum  = 0.0f;
			Frames = 0;
		}
	}
}
