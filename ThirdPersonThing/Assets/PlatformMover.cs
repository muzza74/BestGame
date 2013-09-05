using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {
	private float startPos;
	private int direction = 1;
	public float MoveOffset = 15f;
	public float MoveSpeed = .3f;
	// Use this for initialization
	void Start () {
		startPos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < startPos - MoveOffset){
			direction = 1;
		}
		if (transform.position.x > startPos + MoveOffset){
			direction = -1;
		}
		transform.Translate(MoveSpeed*direction*Time.deltaTime,0,0);
	}
}
