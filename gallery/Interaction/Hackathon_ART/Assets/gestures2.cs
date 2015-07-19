using UnityEngine;
using System.Collections;
using Leap;

public class gestures2 : MonoBehaviour {
	GameObject plane;
	Controller controller; 
	//which images to combine
	//-1 is no selection
	int first_img = -1;
	int second_img = -1;
	Vector3 origin = Vector3.zero;

	// Use this for initialization
	void Start () {
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
		controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 0.1f);
		controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.2f);
		controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		Frame curr_frame = controller.Frame(); //The latest frame
		//check if gesture happened
		GestureList many_gestures = curr_frame.Gestures();
		if (many_gestures.Count > 0) 
		{
			//go through the gestures
			foreach (var a in curr_frame.Gestures())
			{
				//if one of them is a tap
				if (a.Type == Gesture.GestureType.TYPESCREENTAP)
				{
					//see which finger was used
					//PointableList which_way = x.Pointables;
					//if (which_way.Count > 0)
					//see which hand was used
					HandList hands = a.Hands;
					if (hands.Count > 0)
					{
						Transform transform = GetComponent<Transform>();
						//convert ray to unity
						Vector3 start = origin;
						//the direction is towards the palmposition
						Vector3 dir = transform.forward;
						//Vector3 start = this_way.TipPosition.ToUnity();
						//Vector3 dir = this_way.Direction.ToUnity();
						//Vector handCenter = hand.PalmPosition;
						Ray pointer = new Ray(start, dir);
						//pointer.origin = this_way.TipPosition;
						//pointer.direction = this_way.Direction;
						//ray cast
						RaycastHit hit;
						Debug.Log("Tap! " + dir.ToString());
						if(Physics.Raycast(pointer, out hit))
						//if (Physics.Raycast(pointer))
						{
							Debug.Log ("Intersect!");
						}
						//Physics.Raycast(start, direction, Mathf.Infinity);
						//check if pointing on this one

					}
				}
			}

		}
	}
}
