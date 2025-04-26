using UnityEngine;
using TMPro;

public class UIEnemiesLeftPresenter : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;// 中の数字を.textで変えるので、TMP_Text型で持ってくる

    const string ENEMIES_LEFT_STRING = "Enemies Left : ";

    public void UpdateEnemiesLeft(int enemiesLeft)
    {
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
    }
}
