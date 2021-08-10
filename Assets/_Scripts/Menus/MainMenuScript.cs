using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{

    public Slider soundSlider;
    public Slider musicSlider;
    public TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;
    public LevelLoadManager levelmanagerHandle;
    // Start is called before the first frame update

    public void Awake()
    {
        CursorScript.SetMenuCursor();
        AudioSource aud = FindObjectOfType<AudioSource>();
        if(aud!= null)
           SoundManager.PlaySound("Menu", ref aud);

        if(musicSlider == null)
        {
            musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        }
        if (soundSlider == null)
        {
            soundSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
        }
        if (resolutionDropDown != null) SetResolutionDropDown();
    }

    private void SetResolutionDropDown()
    {
        int currenResIndex = 0;
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currenResIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currenResIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height,Screen.fullScreen);

    }

    public void StartTheGame()
    {
        levelmanagerHandle.LoadLevel_1();


    }
    public void ExitGame()
    {
        Application.Quit();
    }


    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.LogWarning(isFullScreen.ToString());
    }

    public void turnoffMusic()
    {
        if (SoundManager.MusicVolume > 0)
            musicSlider.value = 0f;
        else
            musicSlider.value = 1f;
    }
    public void turnoffEffects()
    {
        if (SoundManager.EffectsVolume > 0)
            soundSlider.value = 0f;
        else
            soundSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(musicSlider != null && soundSlider != null)
        {
            SoundManager.MusicVolume = musicSlider.value;
            SoundManager.EffectsVolume = soundSlider.value;
        }


    }


}
