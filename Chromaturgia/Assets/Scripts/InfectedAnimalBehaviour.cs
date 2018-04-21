using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedAnimalBehaviour : MonoBehaviour {

	Animator animator;
	Color colors;
	Color laserColor;
	bool win;
	static int totalAnimals = 0;
	static int curedAnimals = 0;
	static Text levelText;

	void Awake ()
	{
		totalAnimals++;
		levelText = GameObject.FindGameObjectWithTag("LevelInfo").GetComponent<Text>();
	}

	void Start () {
		UpdateText ();
		colors = gameObject.GetComponentInChildren<SpriteRenderer> ().color;
		win = false;

		animator = gameObject.GetComponent<Animator> ();
		InvokeRepeating ("AnimacionIdle2", 0, 5f);
	}
		
	void Update () {
		if (!win && colors == Color.white) {
			win = true;
			curedAnimals++;
			UpdateText ();

            GameManager.instance.SetPuzzleAsCompleted();
		}
	}

	void UpdateText()
	{
		levelText.text = (curedAnimals.ToString ()) + "  de  " + (totalAnimals.ToString ());
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!win && coll.gameObject.tag == "Laser") {
			laserColor = coll.gameObject.GetComponent<SpriteRenderer> ().color;
			if (laserColor == Color.red) {
				colors.r = 1;
			}
			else if (laserColor == Color.green) {
				colors.g = 1;
			}
			else if (laserColor == Color.blue) {
				colors.b = 1;
			}
			gameObject.GetComponentInChildren<SpriteRenderer> ().color = colors;
			Destroy (coll.gameObject);
		}
	}

	void AnimacionIdle2()
	{
		animator.SetTrigger ("Idle2");
	}
}
