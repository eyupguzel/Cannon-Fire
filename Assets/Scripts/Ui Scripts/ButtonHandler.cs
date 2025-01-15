using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : IButtonAction
{
    public void Execute()
    {
        AudioManager.Instance.ClikSound();
        LevelManager.Instance.currentLevel = SaveSystem.data.lastLevel;
        SceneManager.LoadScene(SaveSystem.data.lastLevel);
        UiManager.Instance.mainPanel.SetActive(false);
        //UiManager.Instance.UpdateLevelText();
    }

}

public class SelectLevelButtonHandler : IButtonAction
{
    public void Execute()
    {
        UiManager.Instance.mainPanel.SetActive(false);
       // UiManager.Instance.selectLevelPanel.SetActive(true);
    }
}

public class SettingsButtonHandler : IButtonAction
{
    public void Execute()
    {
        // Open settings panel...
    }
}
public class ExitButtonHandler : IButtonAction
{
    public void Execute()
    {
        AudioManager.Instance.ClikSound();

        Application.Quit();
    }
}

public class NextLevelButtonHandler : GameData ,IButtonAction
{
    public void Execute()
    {
        Debug.Log(LevelManager.Instance.currentLevel);
        if(LevelManager.Instance.currentLevel <= SaveSystem.data.lastLevel)
        {
            if (LevelManager.Instance.currentLevel == SaveSystem.data.lastLevel)
                SaveSystem.data.lastLevel++;

           SceneManager.LoadScene(++LevelManager.Instance.currentLevel);
        }
        else
        SceneManager.LoadScene(++SaveSystem.data.lastLevel);

        Debug.Log(LevelManager.Instance.currentLevel);
    }
}
public class RestartButtonHandler : IButtonAction
{
    public void Execute()
    {
        GameManager.Instance.DeactivateBalls();
        SceneManager.LoadScene(LevelManager.Instance.currentLevel);
    }
}

public class BackToMainMenu : IButtonAction
{
    public void Execute()
    {
        UiManager.Instance.mainPanel.SetActive(true);
        EventManager.Instance.OnBackMenu();
        SceneManager.LoadScene(1);
    }
}