var lookSpeed = 15.0;
var moveSpeed = 10.0;
 
var rotationX = 0.0;
var rotationY = 0.0;


function Update ()
{
	rotationX += Input.GetAxis("Mouse X")*lookSpeed;
	rotationY += Input.GetAxis("Mouse Y")*lookSpeed;
	rotationY = Mathf.Clamp (rotationY, -90, 90);
 
	transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
	transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);


    var move_vec : Vector3 = Vector3.zero;	    
	move_vec += transform.forward * Input.GetAxis("Vertical");
	move_vec += transform.right * Input.GetAxis("Horizontal");
	
	//Debug.Log( move_vec );
	
	GetComponent.<Rigidbody>().AddForce( move_vec * moveSpeed );
	
	
}