using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour {

    Slider playerLight;
    enum Option {Red, Green, Blue};
    Option colorSelect = new Option();

    void Awake () {
		// initialization
		playerLight = GetComponent<Slider> ();

		if (gameObject.tag == "RedSlider") 
		{
            colorSelect = Option.Red;
		}
		else if (gameObject.tag == "GreenSlider") 
		{
            colorSelect = Option.Green;
        }
		else if (gameObject.tag == "BlueSlider") 
		{
            colorSelect = Option.Blue;
        }
	}

	void Update () 
	{
		if (colorSelect == Option.Red)
			playerLight.value = GameManager.instance.colors.x;
		else if (colorSelect == Option.Green)
			playerLight.value = GameManager.instance.colors.y;
		else if (colorSelect == Option.Blue)
			playerLight.value = GameManager.instance.colors.z;
	}
}
