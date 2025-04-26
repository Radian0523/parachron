using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemiesLeftManager enemiesLeftManager;
    [SerializeField] GameObject youWinText; // SetActiveするだけなのでGameObjectを拾ってくる

    public EnemiesLeftManager EnemiesLeftManager => enemiesLeftManager;

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor");
        Application.Quit();
    }

    public void StartHitStopCoroutine(float duration)
    {
        StartCoroutine(HitStopCoroutine(duration));
    }

    IEnumerator HitStopCoroutine(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}
