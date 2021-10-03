using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LevelEndMenu : MonoBehaviour
{

    public GameObject EndGameMenu;
    public GameObject LevelFinishText;
    public Scene activeScene;
    [SerializeField] private Menu levelEndMenu = new Menu();


    private void Awake()
    {
        levelEndMenu.ShowMenu(false);
        SetMenuElements();
    }

    private void SetMenuElements()
    {
        foreach (MenuElement el in levelEndMenu.m_elements)
        {
            switch (el.name)
            {
                case "Exit":
                    el.element.GetComponent<Button>().onClick.AddListener(QuitGame);
                    break;
                case "MainMenu":
                    el.element.GetComponent<Button>().onClick.AddListener(MainMenu);
                    break;
                case "NextLevel":
                    el.element.GetComponent<Button>().onClick.AddListener(NextLevel);
                    break;
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Contains("Player"))
        {
            activeScene = SceneManager.GetActiveScene();
            LevelFinishText.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name + " Completed!!!";
            levelEndMenu.ShowMenu(true);
            GameManager.Instance.eventsManager.LevelEnd.Invoke();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(activeScene.buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu()
    {
        Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }

}
