using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLogic : MonoBehaviour {

	Vector2 direction;
	public ShardBehavior shardPrefab;
	ShardBehavior shardClone;
	Color laserColor;
	Animator animator;
	Animator animOrb;
	Rigidbody2D rb2d;

    [HideInInspector]
	public float speed = 100;

	void Start ()
    {
		direction = gameObject.transform.up;

		//caching
		laserColor = gameObject.GetComponent<SpriteRenderer> ().color;
		animator = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();

		rb2d.AddForce (direction * speed, ForceMode2D.Force);
	}

	void OnTriggerEnter2D (Collider2D coll)
    {
		if (coll.gameObject.tag == "Orb")
        {
            animOrb = coll.GetComponent<Animator>();
            bool orbState;

            if (animOrb.GetCurrentAnimatorStateInfo(0).IsName("Off"))
            {
                orbState = true;
            }
            else
            {
                orbState = false;
            }
            coll.GetComponent<OrbLeverBehaviour>().SendSignal();
            animOrb.SetBool("On", orbState);
        }

        if (coll.gameObject.tag == "Turret" || coll.gameObject.tag == "Door" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "NPCBody" || coll.gameObject.tag == "Orb")
		{
			rb2d.velocity = Vector2.zero;
			animator.SetBool ("Destroyed",true);
			Destroy (gameObject, 0.35f);
		}

		if (coll.gameObject.tag == "Player")
		{
			if (laserColor  == Color.red)
            {
                GameManager.instance.IncreaseColor(GameManager.Option.Red);
            }
            else if (laserColor == Color.blue)
            {
                GameManager.instance.IncreaseColor(GameManager.Option.Blue);
            }
            else if(laserColor == Color.green)
            {
                GameManager.instance.IncreaseColor(GameManager.Option.Green);
            }
		}
        
        if (coll.gameObject.tag == "Opaque" || (coll.gameObject.tag == "RedTransparent" && laserColor != Color.red) || (coll.gameObject.tag == "GreenTransparent" && laserColor != Color.green) || (coll.gameObject.tag == "BlueTransparent" && laserColor != Color.blue))
		{
            rb2d.velocity = Vector2.zero;
            animator.SetBool("Destroyed", true);
            Invoke("CrearShards", 0.35f);
            Destroy(gameObject, 0.35f);
        }
    }

    public void CrearShards()
	{
		shardClone = Instantiate(shardPrefab, gameObject.transform.position, Quaternion.identity);
		shardClone.GetComponent<SpriteRenderer> ().color = laserColor;
	}

}
