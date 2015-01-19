using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public float speed = 5.0F;
	public float smooth = 5.0F;
	public Vector3 endPosition;  //Then end position of the elevator
	
	private Vector3 startPosition;
	private float startTime;
	private float journeyLength;
	private bool isLerping = false;  //Used to distinugish when the lerping should happen
	private Transform parent;

	// Use this for initialization
	void Start () {
		//Set our inital variables
		parent = transform.parent;  //A reference is made so we dont have to search for this parent each frame later
		startPosition = parent.position;  //Used to know where our lerp is starting from
	}
	
	/**
	 * We use FixedUpdate instead of Update as the constant Lerp updating can get a little jittery if
	 * used on regular frame updates.
	 **/
	void FixedUpdate () {
		if(isLerping) {
			//If the isLerping bool is true, Lerp to the endPosition
			LerpToPosition();
			CheckLerpComplete();
		}
	}

	void OnTriggerEnter(Collider collide) {
		if(collide.tag == "Player") {
			//Catches wehen the player has stepped on to the elevator
			StartLerp();
		}
	}

	/**
	 * This method will get all attributes needed for the lerping set up and ready
	 **/
	void StartLerp() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, endPosition);
		isLerping = true;
	}

	/**
	 * Called every frame once the isLerping bool is set to true
	 **/
	void LerpToPosition() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		parent.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
	}

	/**
	 * Checks to see if the lerp has reached its end position, if so, stop calling the lerp function.
	 * Only called if isLerping is set to true
	 **/
	void CheckLerpComplete() {
		if (parent.position == endPosition) {
			isLerping = false;
		}
	}
}
