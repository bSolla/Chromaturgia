using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour 
{
	Color turretColor;
	public float frecuency = 2f;
	public float laserSpeed = 50f;
	public Transform gun;
	public EnemyLaserLogic varPrefab;
	EnemyLaserLogic clon;
	Animator animator;

	void Start () 
	{
		turretColor = gameObject.GetComponentInChildren <SpriteRenderer> ().color;
		animator = gameObject.GetComponentInChildren<Animator> ();
		InvokeRepeating ("Shoot", 0f, frecuency);
	}
		
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Laser") 
		{
			if (coll.gameObject.GetComponent <SpriteRenderer> ().color == turretColor) 
			{
				Destroy (coll.gameObject);
				animator.SetBool ("Destroyed", true);
				Destroy (gameObject,0.75f);
			}
			else if (coll.gameObject.tag != "Gun")
            {
                Destroy(coll.gameObject);
            }
		}
	}

	void Shoot ()
	{
		clon = Instantiate (varPrefab, gun.transform.position ,gameObject.transform.rotation);

		clon.speed = laserSpeed;
        if (turretColor == Color.red)
        {
            clon.turretColor = GameManager.Option.Red;
        }
        else if (turretColor == Color.blue)
        {
            clon.turretColor = GameManager.Option.Blue;
        }
        else
        {
            clon.turretColor = GameManager.Option.Green;
        }

		animator.SetTrigger ("Shoot");
	}
}
