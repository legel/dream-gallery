using UnityEngine;
using System.Collections;
using Leap;

public class gestures : MonoBehaviour {
	Controller controller;
	// Use this for initialization
	void Start () {
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
	}
	
	// Update is called once per frame
	void Update () {
		Frame curr_frame = controller.Frame(); //The latest frame
		//check if gesture happened
		GestureList many_gestures = curr_frame.Gestures();
		if (many_gestures.Count > 0) 
		{
			//go through the gestures
			foreach (var x in curr_frame.Gestures())
			{
				//if one of them is a tap
				if (x.Type == Gesture.GestureType.TYPESCREENTAP)
				{
					//see which finger was used
					PointableList which_way = x.Pointables;
					if (which_way.Count > 0)
					{
						Pointable this_way = which_way[0];
						//see which painting it points at
						Vector start = this_way.TipPosition;
						Vector direction = this_way.Direction;

						Debug.Log("Tap!\n");
					}
				}
			}

		}
	}
}
