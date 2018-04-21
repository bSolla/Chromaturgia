using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public Sprite redHUD, greenHUD, blueHUD;

	Image currentHUD;

	void Start () 
	{
		currentHUD = gameObject.GetComponent<Image> ();
	}

	public void ChangeColorSelected(string color) {
		if (color == "R") 
		{
			currentHUD.sprite = redHUD;
		}
		else if (color == "G") 
		{
			currentHUD.sprite = greenHUD;
		}
		else if (color == "B") 
		{
			currentHUD.sprite = blueHUD;
		}
	}

	void CheckInput () {
		if (GameManager.instance.chosenColor == GameManager.Option.Red) 
		{
			ChangeColorSelected ("R");
		} 
		else if (GameManager.instance.chosenColor == GameManager.Option.Green) 
		{
			ChangeColorSelected ("G");
		} 
		else if (GameManager.instance.chosenColor == GameManager.Option.Blue) 
		{
			ChangeColorSelected ("B");
		}
	}

	void Update () 
	{
        if (!PauseMenu.GameIsPaused)
        {
            CheckInput();
        }
	}
}
