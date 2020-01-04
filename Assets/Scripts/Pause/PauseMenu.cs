using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public EventSystem ES;
    private GameObject storeSelected;

    public GameObject PauseUI;

    private bool paused = false;

    public AudioSource pauseSounds;

    void Start()
    {
        PauseUI.SetActive(false);

        storeSelected = ES.firstSelectedGameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            pauseSounds.Play();
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        if(ES.currentSelectedGameObject != storeSelected)
        {
            if (ES.currentSelectedGameObject == null)
                ES.SetSelectedGameObject(storeSelected);
            else
                storeSelected = ES.currentSelectedGameObject;
        }
    }

    public void Resume()
    {
        paused = false;
    }


    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
