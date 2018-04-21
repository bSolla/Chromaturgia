using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	Vector2 movementDirection;
	Rigidbody2D playerRB;
	Animator playerAnimator;
	GunController gun;
	Transform gunTransform;

	bool moves;

	Vector3 rightRotation, leftRotation, upRotation, downRotation;
	Vector3 rightPosition, leftPosition, upPosition, downPosition;
	public float gunPosUp, gunPosDown, gunPosLeft, gunPosRight;

	// player speed
	public float speed;

	void Start () 
	{
		// caching 
		playerRB = gameObject.GetComponent<Rigidbody2D> ();
		playerAnimator = gameObject.GetComponent<Animator> ();
		gun = gameObject.GetComponentInChildren<GunController> ();
		gunTransform = gun.transform;

		// initializing
		moves = false;
		InitializeVectors ();
	}

	// sets position and rotation vectors for the gun
	void InitializeVectors() 
	{
		upRotation = Vector3.zero;
		leftRotation = new Vector3 (0, 0, 90);
		downRotation = new Vector3 (0, 0, 180);
		rightRotation = new Vector3 (0, 0, 270);

		upPosition = new Vector3 (0, gunPosUp, 0);
		downPosition = new Vector3 (0, gunPosDown, 0);
		leftPosition = new Vector3 (gunPosLeft, 0.066f, 0);
		rightPosition = new Vector3 (gunPosRight, 0.066f, 0);
	}

	// applies an impulse with a module of (direction*speed)
	void Move(Vector2 direction) 
	{
		SetAnimatorBools ("Move");

		playerRB.AddForce(direction*speed, ForceMode2D.Impulse);
	}

	// sets the direction bool inside the player animator
	void Orientation(Vector2 orientation) {
		// avoids having to assign false to the remaining directions inside each if
		playerAnimator.SetBool ("Up", false);
		playerAnimator.SetBool ("Down", false);
		playerAnimator.SetBool ("Right", false);
		playerAnimator.SetBool ("Left", false);

		// sets the corresponding bool to true 
		if (orientation == Vector2.up) {
			playerAnimator.SetBool ("Up", true);
		}
		else if (orientation == Vector2.down) {
			playerAnimator.SetBool ("Down", true);
		}
		else if (orientation == Vector2.right) {
			playerAnimator.SetBool ("Right", true);
		}
		else if (orientation == Vector2.left) {
			playerAnimator.SetBool ("Left", true);
		}
	}

	// checks wether or not the specified boolean is already set to true or not, and if not, sets it
	void SetAnimatorBools(string name) {
		if (!playerAnimator.GetBool("Move") && name == "Move") {
			playerAnimator.SetBool ("Move", true);
		}
		else if (!playerAnimator.GetBool("Idle") && name == "Idle") {
			playerAnimator.SetBool ("Move", false);
		}
		else if (!playerAnimator.GetBool("Right") && name == "Right") {
			Orientation (Vector2.right);
		}
		else if (!playerAnimator.GetBool("Left") && name == "Left") {
			Orientation (Vector2.left);
		}
		else if (!playerAnimator.GetBool("Up") && name == "Up") {
			Orientation (Vector2.up);
		}
		else if (!playerAnimator.GetBool("Down") && name == "Down") {
			Orientation (Vector2.down);
		}
	}

	void SetGunOrientation(Vector2 orientation) {
		if (orientation == Vector2.up) {
			gunTransform.eulerAngles = upRotation;
			gunTransform.localPosition = upPosition;
		} 
		else if (orientation == Vector2.left) {
			gunTransform.eulerAngles = leftRotation;
			gunTransform.localPosition = leftPosition;
		}
		else if (orientation == Vector2.down) {
			gunTransform.eulerAngles = downRotation;
			gunTransform.localPosition = downPosition;
		}
		else if (orientation == Vector2.right) {
			gunTransform.eulerAngles = rightRotation;
			gunTransform.localPosition = rightPosition;
		}
	}

	// checks for input and if it's one of the direction keys, assigns the corresponding values to the Vector2 movementDirection and 
	// sets the boolean move to true
	void CheckInput() {
		if (Input.GetKey ("up")) {
			movementDirection.x = 0;
			movementDirection.y = 1;
			moves = true;
			SetAnimatorBools ("Up");
		} else if (Input.GetKey ("down")) {
			movementDirection.x = 0;
			movementDirection.y = -1;
			moves = true;
			SetAnimatorBools ("Down");
		} else if (Input.GetKey ("right")) {
			movementDirection.x = 1;
			movementDirection.y = 0f;
			moves = true;
			SetAnimatorBools ("Right");
		} else if (Input.GetKey ("left")) {
			movementDirection.x = -1;
			movementDirection.y = 0;
			moves = true;
			SetAnimatorBools ("Left");
		}
		else if (Input.GetKeyUp ("up") || Input.GetKeyUp("down") || Input.GetKeyUp("right") || Input.GetKeyUp("left")) {
			moves = false;
			SetAnimatorBools ("Idle");
		}

	}

	// check input every frame
	void Update () {
		CheckInput ();
	}

	// if there's input, apply force (impulse)
	void FixedUpdate() {
		if (moves) {
			Move (movementDirection);
			SetGunOrientation (movementDirection);

			// reset inertia so it doesn't spin uncontrollably
			playerRB.inertia = 0f;
		}
	}
}
