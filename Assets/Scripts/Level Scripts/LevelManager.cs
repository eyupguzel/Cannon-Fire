using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
    [HideInInspector] public int currentLevel;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(SaveSystem.data.lastLevel == 0)
        {
            SaveSystem.data.lastLevel = 1;
            currentLevel = SaveSystem.data.lastLevel;
            SaveSystem.Instance.SaveData();
        }

        StartCoroutine(LoadAsync(SaveSystem.data.lastLevel));
    }
    public void SelectLevel(int i)
    {
        currentLevel = i;
        SceneManager.LoadScene(i);

    }
    private IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        if (!operation.isDone)
           yield return null;
    }
    public void SetCurrentLevel() => currentLevel = SceneManager.GetActiveScene().buildIndex;
}
