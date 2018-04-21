using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismMouthBehaviour : MonoBehaviour {

	LaserLogic laserShot;
	public LaserLogic laserPrefab;
	public float laserSpeed = 100;

	public void ShootLaser(Color laserColor) 
	{
		laserShot = Instantiate (laserPrefab, gameObject.transform.position, gameObject.transform.rotation);
		laserShot.GetComponent<SpriteRenderer> ().color = laserColor;
		laserShot.speed = laserSpeed;
	}
}