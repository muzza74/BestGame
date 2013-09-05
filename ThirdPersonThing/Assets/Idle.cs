using UnityEngine;
using System.Collections;


public class Idle : StateScript {
	public float decayRate = 0.2f;
	
	void Start(){
		theController = GetComponent<CharacterMoveControl>();

	}
	
	public override void StateUpdate(){
		Debug.Log("Idle");
		//if a key is pressed
		if(Input.anyKey||isKeyDirection()){
			//if there is any movement
			if (isKeyDirection()){
				theController.changeState(State.Walking);
			}
		} else {
			decayMovement();
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
	
	private void decayMovement(){
		// checks to see if the character is moving faster than the
		// rate of movement speed decay
		if(theController.moveSpeed > decayRate){
			theController.moveSpeed -= decayRate;
		// if it's moving slower than the decay rate, set the 
		// move speed to zero
		} else {
			theController.moveSpeed = 0f;
		}
	}
}
