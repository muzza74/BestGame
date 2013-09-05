using UnityEngine;
using System.Collections;

public abstract class StateScript : MonoBehaviour {
	
	public CharacterMoveControl theController;
	
	public abstract void StateUpdate();
}
