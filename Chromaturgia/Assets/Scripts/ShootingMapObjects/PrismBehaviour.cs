using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismBehaviour : MonoBehaviour {

	PrismMouthBehaviour mouth;
	Animator animator;

	void Start()
	{
		mouth = gameObject.GetComponentInChildren<PrismMouthBehaviour> ();
		animator = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D coll) 
	{
		if (coll.gameObject.tag == "Laser") 
		{
			Color laserColor = coll.GetComponent<SpriteRenderer> ().color;

			mouth.ShootLaser (laserColor);

			if (laserColor == Color.red)
				animator.SetTrigger ("ShootRed");
			else if (laserColor == Color.green)
				animator.SetTrigger ("ShootGreen");
			else if (laserColor == Color.blue)
				animator.SetTrigger ("ShootBlue");

			Destroy (coll.gameObject);
		}
	}


}

