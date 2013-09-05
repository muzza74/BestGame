using UnityEngine;
using System.Collections;


public enum State{
		Idle,
		Walking,
		Jumping,
		Falling
};
public class CharacterMoveControl : MonoBehaviour {
	
	private int currentState;
	private int prevState;
	
	public float colliderRadius = 0.75f;
	
	// Movement
	public float gravity = 1f;
	public Vector3 moveDirection = Vector3.zero;
	public Vector3 targetDirection = Vector3.zero;
	public float moveSpeed = 0f;
	public float maxSpeed = 5.0f;
	public float rotSpeed = 10f;
	public Vector3 velocity = Vector3.zero;
	public bool IsJumping = false;
	//The deadzone for analogue sticks
	public float deadZone = 0.2f;
	
	//The array of scripts for handling states
	public StateScript[] stateScripts = new StateScript[4];
	
	void Start () {
		//Start the character in Idle
		currentState = (int)State.Idle;
		prevState = currentState;
		moveDirection = transform.TransformDirection(Vector3.forward);
	}
	
	void Update () {
		Debug.Log(currentState.ToString());
		canJump();
		//Call the StateUpdate of the current state
		stateScripts[currentState].StateUpdate();
		rotateCharacter();
		moveCharacter();
		Debug.Log(isGrounded().ToString());
	}
	
	public void changeState(State stateChange){
		currentState = (int)stateChange;
	}
	
	// Handles the rotation of the character
	private void rotateCharacter(){
		float tempRotSpeed = rotSpeed;
		if (currentState == (int)State.Falling){
			tempRotSpeed = tempRotSpeed/2;
		}
		moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, tempRotSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
		moveDirection = moveDirection.normalized;
		transform.rotation = Quaternion.LookRotation(moveDirection);
	}
	
	// Handles the movement of the character
	private void moveCharacter(){
		Vector3 prevPosition = transform.position;
		velocity = (Vector3.forward*moveSpeed*Time.deltaTime);
		velocity.y = -gravity*Time.deltaTime;
		if(isGrounded()&&!IsJumping){
			velocity.y = 0;
			slopeAlign();
		}
		transform.Translate(velocity);
	}
	
	public bool isGrounded(){
		// Checks whether the sphere collides with anything next movement/frame, hence the +velocity
		Collider[] collisions = Physics.OverlapSphere(transform.position+velocity, .76f);
		foreach(Collider tempCollider in collisions){
			if(!tempCollider.Equals(gameObject.collider)){
				Debug.Log("Grounded");
				return true;
			}
		}
		Debug.Log("Not Grounded");
		return false;
	}
	
	public void slopeAlign(){
//		RaycastHit hit;
//		if (Physics.Raycast(transform.position, -Vector3.up, out hit)){
//            velocity.y -= hit.distance - colliderRadius;
//		}
		float minDistance = 0;
		float currentDistance = 0;
		Collider[] collisions = Physics.OverlapSphere(transform.position+velocity, colliderRadius);
		foreach(Collider tempCollider in collisions){
			if(!tempCollider.Equals(gameObject.collider)){
				Vector3 closestPoint = tempCollider.ClosestPointOnBounds(gameObject.transform.position);
				currentDistance = transform.position.y - closestPoint.y;
				if (currentDistance < minDistance){
					minDistance = currentDistance;
					velocity.y = transform.position.y+currentDistance;
				}
			}
		}
	}
	
	private void canJump(){
		if(Input.GetButton("Jump") && !IsJumping && isGrounded()){
			changeState(State.Jumping);
		}
	}
	
	public void SetJumping(bool isJumping){
		IsJumping = isJumping;
	}
}
