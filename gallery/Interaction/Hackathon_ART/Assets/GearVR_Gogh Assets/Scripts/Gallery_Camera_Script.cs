using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gallery_Camera_Script : MonoBehaviour
{

		readonly float RAYCAST_INTERVAL = .5f;
		readonly int HIT_THRESHOLD = 5;
		public LayerMask raycastMask;
		Camera myCamera;
		float myFOV;
	public Text fovText;
		// Use this for initialization
		void Start ()
		{
				InvokeRepeating ("Raycast", RAYCAST_INTERVAL, RAYCAST_INTERVAL);
				myCamera = GetComponent<Camera> ();
				myFOV = myCamera.fieldOfView;
				myCamera.fieldOfView=myFOV;

				fovText.text = "FOV:" + myCamera.fieldOfView.ToString ();

		}

		RaycastHit hit;
		string lastHitName;
		int hitStreak;
		bool isMorphing;

		void Raycast ()
		{
				if (isMorphing)
						return;
				if (Physics.Raycast (transform.position, transform.forward, out hit, 10f, raycastMask)) {
						GameObject colliderObject = hit.collider.gameObject;
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
				} else {
						hitStreak = 0;
						lastHitName = "";
				}
		}
		// Update is called once per frame
		void Update ()
		{
				if (isMorphing) {
			myFOV-=Time.deltaTime*10f;
			myCamera.fieldOfView=myFOV;
			if (myCamera.fieldOfView < 30){
				isMorphing = false;
			}


				}
		fovText.text = "FOV:" + myCamera.fieldOfView.ToString ();

		}
}
