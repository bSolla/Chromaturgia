using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	
	public LaserLogic prefabRed, prefabGreen, prefabBlue;
    public float laserSpeed = 100;

	float bulletQuantity;
    LaserLogic laserShot;

	void Start()
	{
		// caching
		bulletQuantity = GameManager.instance.bulletAmount;
	}

	bool CanShoot()
    {
        bool canShoot = false;

		if (GameManager.instance.currentAction == GameManager.Action.Shoot) 
		{
			if (GameManager.instance.chosenColor == GameManager.Option.Red && GameManager.instance.colors.x < bulletQuantity) 
			{
			} else if (GameManager.instance.chosenColor == GameManager.Option.Green && GameManager.instance.colors.y < bulletQuantity) 
			{
			} else if (GameManager.instance.chosenColor == GameManager.Option.Blue && GameManager.instance.colors.z < bulletQuantity) 
			{
			} else 
			{
				canShoot = true;
			}
		} 
        return canShoot;
    }

	public void ShootLaser()
    {
        if (CanShoot ())
        {
            if (GameManager.instance.chosenColor == GameManager.Option.Red)
            {
                laserShot = Instantiate(prefabRed, gameObject.transform.position, gameObject.transform.rotation);
            }
            else if (GameManager.instance.chosenColor == GameManager.Option.Green)
            {
                laserShot = Instantiate(prefabGreen, gameObject.transform.position, gameObject.transform.rotation);
            }
			else if (GameManager.instance.chosenColor == GameManager.Option.Blue)
            {
                laserShot = Instantiate(prefabBlue, gameObject.transform.position, gameObject.transform.rotation);
            }
            laserShot.speed = laserSpeed;
			GameManager.instance.DecreaseColor (GameManager.instance.chosenColor);
		}
	}

    void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameManager.instance.chosenColor = GameManager.Option.Red;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            GameManager.instance.chosenColor = GameManager.Option.Green;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            GameManager.instance.chosenColor = GameManager.Option.Blue;
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("Wall") && coll.gameObject.tag == "Opaque")
        {
            GetComponentInParent<PlayerInputs>().gunEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("Wall") && coll.gameObject.tag == "Opaque")
        {
            GetComponentInParent<PlayerInputs>().gunEnabled = true;
        }
    }

    private void Update()
    {
        CheckInput();
    }
}
