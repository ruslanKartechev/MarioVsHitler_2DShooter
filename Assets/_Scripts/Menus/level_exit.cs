using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class level_exit : MonoBehaviour
{
    public GameObject EndGameMenu;
    public GameObject LevelFinishText;
    public Scene activeScene;
    private float ceeling;
    private float floor;
    public float offset = 0.2f;
    private bool goUp = true;
    public float bouncingSpeed = 0.5f;
    private Vector3 unitY = new Vector3(0, 1, 0);




    private void Awake()
    {
    }


    void Start()
    {
        if(GetComponent<CircleCollider2D>() != null)
        {
            offset = (float)GetComponent<CircleCollider2D>().radius;
        }
        ceeling = transform.position.y + offset;
        floor = transform.position.y - offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.position += unitY * bouncingSpeed * Time.deltaTime;

        }
        else if (!goUp)
        {
            transform.position -= unitY * bouncingSpeed * Time.deltaTime;
        }

        if (transform.position.y >= ceeling)
        {
            goUp = false;
        }
        else if (transform.position.y <= floor)
        {
            goUp = true;
        }

     

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Contains("Player"))
        {
            activeScene = SceneManager.GetActiveScene();
            LevelFinishText.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name + " Completed!!!";
            EndGameMenu.SetActive(true);
            Time.timeScale = 0;
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


    public void LoadMenu()
    {
        Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
        SceneManager.LoadScene("MainMenu");

    }






}
