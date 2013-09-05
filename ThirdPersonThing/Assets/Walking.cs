using UnityEngine;
using System.Collections;

public class Walking : StateScript {
	public float walkAcceleration = 0.4f;
	public float runAcceleration = 0.6f;
	public Camera theCamera;
	
	void Start(){
		theController = GetComponent<CharacterMoveControl>();

	}
	
	public override void StateUpdate(){
		Debug.Log("Walking");
		if(Input.anyKey||isKeyDirection()){
			if(isKeyDirection()){
				updateMoveSpeed();
				updateRotation();
			}
		} else {
			theController.targetDirection = theController.transform.forward;
			theController.changeState(State.Idle);
		}
	}
	
	private bool isKeyDirection(){
		if (Input.GetAxis("Horizontal") > theController.deadZone ||
			Input.GetAxis("Horizontal") < -theController.deadZone||
			Input.GetAxis("Vertical") > theController.deadZone||
			Input.GetAxis("Vertical") < -theController.deadZone){
			return true;
		}
		return false;
	}
	
	// Updates the movement speed of the character based on the parameters passed
	private void updateMoveSpeed(){
		if (theController.moveSpeed > theController.maxSpeed){
			theController.moveSpeed = theController.maxSpeed;
		} else {
			theController.moveSpeed += walkAcceleration;
		}
	}
	
	private void updateRotation(){
		Transform cameraTransform = theCamera.transform;
		// forward of the camera on the x-z plane
		Vector3 tempTarget = Input.GetAxis("Vertical")
			* theCamera.transform.forward + Input.GetAxis("Horizontal") * theCamera.transform.right;
		tempTarget.y = 0.0f;
		if (tempTarget!= Vector3.zero){
			theController.targetDirection = tempTarget;
		}
	}
}
