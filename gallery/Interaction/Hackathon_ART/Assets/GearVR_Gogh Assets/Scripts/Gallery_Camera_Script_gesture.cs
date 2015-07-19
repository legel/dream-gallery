using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class Gallery_Camera_Script_gesture : MonoBehaviour
{

		readonly float RAYCAST_INTERVAL = .5f;
		readonly int HIT_THRESHOLD = 5;
		public LayerMask raycastMask;
		Controller controller; 
		Camera myCamera;
		float myFOV;
	GameObject colliderObject;
	public Text fovText;
	GameObject painting;
		// Use this for initialization
		void Start ()
		{
				controller = new Controller();
				InvokeRepeating ("Raycast", RAYCAST_INTERVAL, RAYCAST_INTERVAL);
				myCamera = GetComponentInChildren<Camera> ();
				myFOV = myCamera.fieldOfView;
				myCamera.fieldOfView=myFOV;

				fovText.text = "FOV:" + myCamera.fieldOfView.ToString ();

				controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
				controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
				//controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 0.01f);
				//controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.2f);
				//controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 0.01f);
		}

		RaycastHit hit;
		string lastHitName;
		int hitStreak;
		bool isMorphing;

		void Raycast ()
		{
				if (isMorphing)
						return;
				Frame curr_frame = controller.Frame(); //The latest frame
				//check if gesture happened
				GestureList many_gestures = curr_frame.Gestures();
				if (many_gestures.Count > 0) 
				{
					//go through the gestures
					//foreach (var a in curr_frame.Gestures())
					{
						//if one of them is a tap
						//if (a.Type == Gesture.GestureType.TYPESCREENTAP)
						{
							//see which finger was used
							//PointableList which_way = x.Pointables;
							//if (which_way.Count > 0)
							//see which hand was used
							//HandList hands = a.Hands;
							//if (hands.Count > 0)
							{
								if (Physics.Raycast (transform.position, transform.forward, out hit, 10f, raycastMask)) {

									 colliderObject = hit.collider.gameObject;
							 painting =hit.collider.transform.parent.gameObject;

										Debug.Log("Ray hit the "+colliderObject.name.ToString());
							//painting.transform.position = Vector3.MoveTowards(colliderObject.transform.position,this.transform.position,10f);
							painting.SetActive(false);			

							if (colliderObject.name.Equals (lastHitName)) {
												hitStreak++;
										} else {
												hitStreak = 0;
										}
										if (hitStreak == HIT_THRESHOLD) {
												hit.collider.GetComponent<Canvas_Script> ().Morph ();
												isMorphing = true;
										}
										lastHitName = colliderObject.name;
								}
							}
						}
					}
				}
				//else {
				//		hitStreak = 0;
				//		lastHitName = "";
				//}
		}
		// Update is called once per frame
		void Update ()
		{
				if (isMorphing) {
			painting.transform.localScale+= new Vector3(30f,30f,30f);			

			//colliderObject.transform.position = Vector3.MoveTowards(colliderObject.transform.position,this.transform.position,0f);
			myCamera.fieldOfView=myFOV;
			if (myCamera.fieldOfView < 30){
				isMorphing = false;
			}


				}
		fovText.text = "FOV:" + myCamera.fieldOfView.ToString ();

		}
}
