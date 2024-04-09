using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIManager : SingletonDontDestroy<UIManager>
{
    [System.Serializable]
    public class UIScreen
    {
        public string tag;
        public GameObject UIPrefab;
        public int type;
       
    }

    public List<Button> mainMenuUIButtons = new List<Button>();
   // public List<Button> gameUiButtons = new List<Button>();
   
    public List<UIScreen> Uis = new List<UIScreen>();


    [SerializeField]
    private float timeScaleValue;

    public bool GameIsPaused = false;
   
    private GameObject currentMenuUI;

    private LevelLoader levelLoader;

  
    
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();

        SetStartingUI();
    }

    private void Update()
    {

    }

    private void SetStartingUI()
    {

        foreach (UIScreen uIScreen in Uis)
        {
            if (uIScreen.type == 1)
            {
                uIScreen.UIPrefab.SetActive(true);
            }
            else
            {
                uIScreen.UIPrefab.SetActive(false);
            }
        }
    }

    #region Pause UI Functions
    public void Pause()
    {
        Time.timeScale = timeScaleValue;
        GameIsPaused = true;

    }
    #endregion

    public void MenuPlay(int index)
    {
        levelLoader.LoadLevel(index);
        if (GameIsPaused)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    public void Restart()
    {
        levelLoader.Reload();
        if (GameIsPaused)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
     
    }



    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("Has quit Application!");
    }

    public void SetInactiveButton()
    {
        foreach (Button button in mainMenuUIButtons)
        {
            button.interactable = false;
        }
    }

    public void SetActiveButton()
    {
        foreach(Button button in mainMenuUIButtons)
        {
            button.interactable = true;
        }

        
    }
    public void UpdateUI(int type)
    {
        foreach (UIScreen ui in Uis)
        {
            if(ui.type != type)
            {
                ui.UIPrefab.SetActive(false);
            }
            else
            {
                ui.UIPrefab.SetActive(true);
                currentMenuUI = ui.UIPrefab;
            }
        }
    }
    
}
