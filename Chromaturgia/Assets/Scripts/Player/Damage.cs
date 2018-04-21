using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	new SpriteRenderer renderer;

    [HideInInspector]
    public Color turretColor = Color.red;

	void Start () 
	{
		renderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void Flash()
	{
        renderer.color = turretColor;
		Invoke ("NormalColor", 0.25f);
	}

	public void NormalColor()
	{
		renderer.color = Color.white;
	}
}
