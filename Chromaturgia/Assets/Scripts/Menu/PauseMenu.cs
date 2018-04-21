using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject pauseMenu;
	public EventSystem eventSystemPauseMenu;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (GameIsPaused)
				resume ();
			else
				pause ();
		}
	}

	public void resume()
	{
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		GameIsPaused = false;
	}

	void pause()
	{
		EventSystem.current = eventSystemPauseMenu;
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		GameIsPaused = true;
	}

	public void loadMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
