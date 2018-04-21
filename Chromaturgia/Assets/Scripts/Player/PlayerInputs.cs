using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour {

	public Sprite Shoot, Act, Talk;

    [HideInInspector]
    public bool gunEnabled = true;
    [HideInInspector]
	public NPCBehaviour npc;
    [HideInInspector]
    public OrbLeverBehaviour orbLever;

	Image currentSprite;
	GunController playerGun;

	void Start () 
	{
		currentSprite = GameObject.FindGameObjectWithTag("Spacebar").GetComponent<Image>();
        playerGun = GameObject.FindGameObjectWithTag ("Gun").GetComponent<GunController> ();
	}

	public void ChangeSprite(GameManager.Action action)
	{
		if (action == GameManager.Action.Shoot) 
		{
			currentSprite.sprite = Shoot;
		}
		else if (action == GameManager.Action.Interact) 
		{
			currentSprite.sprite = Act;
		}
		else if (action == GameManager.Action.Talk) 
		{
			currentSprite.sprite = Talk;
		}
	}

	void CheckInput ()
	{
		if (Input.GetKeyUp (KeyCode.Space))
		{
			if (GameManager.instance.currentAction == GameManager.Action.Shoot && gunEnabled) 
			{
				playerGun.ShootLaser ();
			} 
			else if (GameManager.instance.currentAction == GameManager.Action.Talk) 
			{
				npc.DisplayText();
			}
			else if (GameManager.instance.currentAction == GameManager.Action.Interact)
			{
				orbLever.SendSignal();
			}
		}
    }

	void Update () 
	{
		ChangeSprite (GameManager.instance.currentAction);
        if (!PauseMenu.GameIsPaused)
        {
            CheckInput();
        }
	}
}
