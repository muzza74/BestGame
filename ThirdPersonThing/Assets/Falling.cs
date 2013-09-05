using UnityEngine;
using System.Collections;

public class Falling : StateScript {
	
	public Camera theCamera;
	// Use this for initialization
	void Start () {
		theController = GetComponent<CharacterMoveControl>();
	}
	
	public override void StateUpdate(){
		Debug.Log("Falling");
		if(Input.anyKey||isKeyDirection()){
			if(isKeyDirection()){
				updateRotation();
			}
		}
		if(theController.isGrounded()){
			theController.changeState(State.Idle);
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
	
	
	private bool isKeyDirection(){
		if (Input.GetAxis("Horizontal") > theController.deadZone ||
			Input.GetAxis("Horizontal") < -theController.deadZone||
			Input.GetAxis("Vertical") > theController.deadZone||
			Input.GetAxis("Vertical") < -theController.deadZone){
			return true;
		}
		return false;
	}
}
