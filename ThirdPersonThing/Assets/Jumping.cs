using UnityEngine;
using System.Collections;

public class Jumping : StateScript {
	
	public float JumpSpeed = 30f;
	private float currentJumpSpeed = 0;
	// Use this for initialization
	void Start () {
		theController = GetComponent<CharacterMoveControl>();
	}

	
	public override void StateUpdate(){
		Debug.Log("Jumping");
		if (currentJumpSpeed <= theController.gravity){
			endJump(State.Falling);
		} else if (!theController.isGrounded()){
			if (!theController.IsJumping){
				startJump();
			}
			updateJumpSpeed();
			theController.velocity.y += currentJumpSpeed;
		} else {
			endJump(State.Idle);
		}
	}
	
	private void startJump(){
		theController.SetJumping(true);
		currentJumpSpeed = JumpSpeed;
	}
	
	private void updateJumpSpeed ()
	{	
		JumpSpeed -= theController.gravity;
	}
	
	private void endJump(State theState){
		theController.SetJumping(false);
		theController.changeState(theState);
	}
	
}
