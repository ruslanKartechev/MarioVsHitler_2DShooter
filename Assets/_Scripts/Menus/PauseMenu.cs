using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject mainPauseUI;
    public GameObject optionsMenuUI;
    public float musicvolume = 1f;
    public float effectsvolume = 1f;
    public Slider musicSlider;
    public Slider soundSlider;
    private bool isPaused = false;
    public bool _isPaused {
        get { return isPaused; }
    }
    // Start is called before the first frame update


    public void Resume()
    {
        pauseCanvas.SetActive(false);
        isPaused = false;
    }

    public void SetPause()
    {
        pauseCanvas.SetActive(true);
        if(mainPauseUI.activeInHierarchy == false)
            mainPauseUI.SetActive(true);
        if(optionsMenuUI.activeInHierarchy == true)
            optionsMenuUI.SetActive(false);
        isPaused = true;
    }

    public void Options()
    {
        mainPauseUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    public void Back()
    {
        mainPauseUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }
    // Update is called once per frame
    public void turnoffMusic()
    {
        if ( PlayerControl.soundmanagerLoaded == true)
        {
              if (SoundManager.MusicVolume > 0)
                   musicSlider.value = 0f;
              else
                   musicSlider.value = 1f;
         }
    }
    public void turnoffEffects()
    {
        if(PlayerControl.soundmanagerLoaded == true)
        {

            if (SoundManager.EffectsVolume > 0)
                soundSlider.value = 0f;
            else
                soundSlider.value = 1f;
        }

    }

    // Update is called once per frame
    void Update()
    { 

        if (PlayerControl.soundmanagerLoaded == true)
        {
            SoundManager.MusicVolume = musicSlider.value;
            SoundManager.EffectsVolume = soundSlider.value;
        }
        else
        {
            return;
        }

    }

    public void MainMenu()
    {
        Destroy( FindObjectOfType<DontDestroyOnLoad>().gameObject);    
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
