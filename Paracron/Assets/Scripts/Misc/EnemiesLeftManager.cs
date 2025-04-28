using UnityEngine;

public class EnemiesLeftManager : MonoBehaviour
{
    [SerializeField] UIEnemiesLeftPresenter uiEnemiesLeftPresenter;
    int enemiesLeft = 0;

    public int EnemiesLeft => enemiesLeft;

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        uiEnemiesLeftPresenter.UpdateEnemiesLeft(enemiesLeft);
        // if (enemiesLeft <= 0) youWinText.SetActive(true);
    }
}
