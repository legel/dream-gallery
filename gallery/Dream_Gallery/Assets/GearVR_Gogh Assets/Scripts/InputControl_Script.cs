using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputControl_Script : MonoBehaviour
{
	//EventHandlers
	public delegate void EventHandler ();

	public delegate void EventHandlerFloat (float delta);
	
	public static event EventHandlerFloat onMouseX;
	public static event EventHandlerFloat onMouseY;
	public static event EventHandler onMouse0Down;
	//PublicVariables

	// Use this for initialization
	void Start ()
	{
	}


	// Update is called once per frame
	void Update ()
	{
		UpdateInput ();
	}

	bool ignoreMouseMovement;

	void UpdateInput ()
	{
		if (Input.GetButtonDown ("Mouse 1")) {

		}
		if (Input.GetButtonDown ("Mouse 0")) {
			ignoreMouseMovement = true;
			if (onMouse0Down != null)
				onMouse0Down ();
		}
		if (Input.GetAxis ("Mouse X") != 0) {
			if (ignoreMouseMovement) {
				ignoreMouseMovement = false;
			} else {
				float mouseX = Input.GetAxis ("Mouse X");

				if (onMouseX != null)
					onMouseX (mouseX);
			}
		}
		if (Input.GetAxis ("Mouse Y") != 0) {
			if (ignoreMouseMovement) {
				ignoreMouseMovement = false;
			} else {
				float mouseY = Input.GetAxis ("Mouse Y");
				
				if (onMouseY != null)
					onMouseY (mouseY);
			}
		}
		if (Input.GetButtonUp ("Mouse 0")) {
			ignoreMouseMovement = true;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Time.timeScale = .2f;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			Time.timeScale = .4f;
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			Time.timeScale = .6f;
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			Time.timeScale = .8f;
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			Time.timeScale = 1f;
		}
	}
}