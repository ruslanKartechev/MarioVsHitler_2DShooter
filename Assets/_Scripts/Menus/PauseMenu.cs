using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool pauseMenuActive = false;
    public GameObject inGameUi;
    public float musicvolume = 1f;
    public float effectsvolume = 1f;
    public Slider musicSlider;
    public Slider soundSlider;

    // Start is called before the first frame update
    void Awake()
    {
        pauseMenuActive = false;
    }

    void Start()
    {       
            pauseMenuUI.SetActive(false);
            inGameUi.SetActive(true);
    }


    public void SetPause()
    {
        Debug.LogWarning("ss");
        pauseMenuActive = true;
        Time.timeScale = 0f;
        inGameUi.SetActive(false);
        pauseMenuUI.SetActive(true);       

    }

    public void Resume()
    {
        pauseMenuActive = false;
        pauseMenuUI.SetActive(false);
        inGameUi.SetActive(true);
        Time.timeScale = 1f;

       
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
