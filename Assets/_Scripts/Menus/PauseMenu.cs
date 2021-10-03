using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject mainPause;
    public GameObject optionsPause;
    public float musicvolume = 1f;
    public float effectsvolume = 1f;
    private Slider musicSlider;
    private Slider soundSlider;
    private bool isPaused = false;

    [SerializeField] private Menu pauseMain = new Menu();
    [SerializeField] private Menu pauseOptions = new Menu();

    public bool _isPaused {
        get { return isPaused; }
    }

    private void Awake()
    {
        SetMenuElements();
    }
    private void SetMenuElements()
    {
        foreach (MenuElement el in pauseMain.m_elements)
        {
            switch (el.name)
            {
                case "Exit":
                    el.element.GetComponent<Button>().onClick.AddListener(QuitGame);
                    break;
                case "MainMenu":
                    el.element.GetComponent<Button>().onClick.AddListener(MainMenu);
                    break;
                case "Resume":
                    el.element.GetComponent<Button>().onClick.AddListener(Resume);
                    break;
                case "Options":
                    el.element.GetComponent<Button>().onClick.AddListener(Options);
                    break;
            }
        }

        foreach (MenuElement el in pauseOptions.m_elements)
        {
            switch (el.name)
            {
                case "MusicVolume":
                    el.element.GetComponent<Button>().onClick.AddListener(MusicOff);
                    break;
                case "MusicSlider":
                    musicSlider = el.element.GetComponent<Slider>();
                    musicSlider.onValueChanged.AddListener(delegate { MusicSlider(musicSlider); });
                    break;
                case "EffectsVolume":
                    el.element.GetComponent<Button>().onClick.AddListener(EffectsOff);
                    break;
                case "EffectsSlider":
                    soundSlider = el.element.GetComponent<Slider>();
                    soundSlider.onValueChanged.AddListener(delegate { EffectsSlider(soundSlider); });
                    break;
                case "Back":
                    el.element.GetComponent<Button>().onClick.AddListener(Back);
                    break;
                case "FullScreen":
                    Button fscreen = el.element.GetComponent<Button>();
                    if(fscreen!=null)
                        fscreen.onClick.AddListener( delegate{ FullScreen(fscreen.enabled); }) ;
                    break;
                case "ResolutionDropdown":
                    Dropdown resolution = el.element.GetComponent<Dropdown>();
                    if(resolution!=null)
                        resolution.onValueChanged.AddListener(delegate { SetResolution(resolution); });
                    break;
            }
        }
    }

    public void SetPause()
    {
        pauseMain.ShowMenu(true);
        if (mainPause.activeInHierarchy == false)
            mainPause.SetActive(true);
        if (optionsPause.activeInHierarchy == true)
            optionsPause.SetActive(false);
        isPaused = true;
    }

    public void Resume()
    {
        pauseMain.ShowMenu(false);
        isPaused = false;
        GameManager.Instance.eventsManager.GameResumed.Invoke();
    }


    public void Options()
    {
        mainPause.SetActive(false);
        optionsPause.SetActive(true);
    }
    public void MainMenu()
    {
        Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }
    public void Back()
    {
        mainPause.SetActive(true);
        optionsPause.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicOff()
    {
        if ( PlayerControl.soundmanagerLoaded == true)
        {
              if (SoundManager.MusicVolume > 0)
                   musicSlider.value = 0f;
              else
                   musicSlider.value = 1f;
         }
    }
    private void MusicSlider(Slider m_slider)
    {
        musicvolume = m_slider.value;
    }
    public void EffectsOff()
    {
        if(PlayerControl.soundmanagerLoaded == true)
        {
            if (SoundManager.EffectsVolume > 0)
                soundSlider.value = 0f;
            else
                soundSlider.value = 1f;
        }
    }
    private void EffectsSlider(Slider m_slider)
    {
        effectsvolume = m_slider.value;
    }
    private void FullScreen(bool mVal)
    {

    
    }
    private void SetResolution(Dropdown mDrop)
    {


    }

    void Update()
    {
        if (PlayerControl.soundmanagerLoaded == true)
        {
            SoundManager.MusicVolume = musicSlider.value;
            SoundManager.EffectsVolume = soundSlider.value;
        }
        else
            return;
    }



}
