using UnityEngine;
using System.Collections;

public class BeakMove : MonoBehaviour {
	
	public int StartDirection = -1;
	public float RotateSpeed = 25f;
	public float RotationOffset = 15f;
	private int direction;
	private float frameCounter = 0f;
	// Use this for initialization
	void Start () {
		direction = StartDirection;
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCounter >= RotationOffset){
			direction = -1;
		}
		if (frameCounter <= -RotationOffset){
			direction = 1;
		}
		frameCounter += direction * Time.deltaTime;
		transform.Rotate(RotateSpeed*Time.deltaTime*direction,0,0);
	}
}
