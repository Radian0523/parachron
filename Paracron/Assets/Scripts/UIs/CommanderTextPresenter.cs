using UnityEngine;
using TMPro;
using System;

public enum CommanderTextState
{
    None,
    FirstContact,
    DestroyingWall,
    LastContact,
}

public class CommanderTextPresenter : MonoBehaviour
{
    [SerializeField] TMP_Text commanderText;
    [SerializeField] CommanderTextTrigger fortressTextTrigger;
    // [SerializeField] CommanderTextTrigger manyEnemyTextTrigger;
    [SerializeField] DestroyableWall destroyableWall;
    [SerializeField] EnemiesLeftManager enemiesLeftManager;
    [SerializeField] int enemiesLeftMaxNum = 40;
    [SerializeField] UIEmergencyWthdrawPresenter uIEmergencyWthdrawPresenter;


    // CommanderTextState currentState = CommanderTextState.FirstContact;

    /* string[] textList = new string[]
    {
        "Can you hear me? It's me — Commander Zeus.", // 0
        "Do you see it? That's their stronghold.", // 1
        "As long as it stands, there will be no peace for our planet.", // 2

        "You're alone out there — but that's all we need.", // 3
        "Leave no trace behind. Complete the mission swiftly and silently.", // 4
        "You've gathered the weapons you need along the way, haven't you?,", // 5
        "Good. You're fully equipped.", // 6

        "From here on, you're on your own.", // 7
        "Move in — wipe them out completely.", // 8

        "", // 9

        "First, clear out the grunts outside the fortress.", // 10

        "", // 11

        "Well done! The gate is open — wipe out everything in your path!", // 12

        "", // 13

        "...Wait, something's wrong.",
        "Sensors are picking up massive enemy reinforcements.",
        "Impossible...! We never anticipated this many...!",

        "Listen carefully — abort the mission.",
        "We can't win this fight. Don't throw your life away!",
        "Today isn't the day. Survive, Agent!",
    }; */

    string[] textList = new string[]
    {
    "聞こえるか、俺だ、ゼウス教官だ。", // 0
    "見えるか？あれが奴らの拠点だ。", // 1
    "あれを潰さぬ限り、この星に平和は訪れない。", // 2

    "お前はたった一人だが──それで十分だ。", // 3
    "痕跡を一切残すな。迅速かつ静かに任務を遂行しろ。", // 4
    "道中で必要な武器は揃えたな？", // 5
    "よし、万全だ。", // 6

    "ここから先は、完全にお前一人の戦いだ。", // 7
    "突入しろ──全てを灰燼に帰せ。", // 8

    "", // 9 （空白：間をとる用）

    "まずは、砦の外にいる雑兵どもを片付けろ。", // 10

    "", // 11 （空白：間をとる用）

    "よくやった！門が開いた。すべてを殲滅せよ！", // 12

    "", // 13 （空白：間をとる用）

    "……待て、何かがおかしい。", // 14
    "センサーが大規模な敵増援を捉えた。", // 15
    "あり得ん……！こんな数、想定外だ……！", // 16

    "よく聞け──作戦中止だ。", // 17
    "これ以上の交戦は不可能だ。命を無駄にするな！", // 18
    "目標は今日じゃない。必ず生き延びろ、エージェント！", // 19
    };


    float timer;

    // float timeSinceLastText = 0f;
    int nextTextIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (0 <= nextTextIndex && nextTextIndex <= 9)
        {
            timer += Time.deltaTime;
            if (timer <= 5f) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
            timer = 0;
        }

        if (10 <= nextTextIndex && nextTextIndex <= 10)
        {
            if (!fortressTextTrigger.text) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
        }

        if (nextTextIndex == 11)
        {
            timer += Time.deltaTime;
            if (timer <= 5f) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
            timer = 0;
        }

        if (12 <= nextTextIndex && nextTextIndex <= 12)
        {
            if (destroyableWall) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
        }

        if (nextTextIndex == 13)
        {
            timer += Time.deltaTime;
            if (timer <= 5f) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
            timer = 0;
        }

        if (14 <= nextTextIndex && nextTextIndex <= 14)
        {
            if (enemiesLeftManager.EnemiesLeft < enemiesLeftMaxNum) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
        }

        if (15 <= nextTextIndex && nextTextIndex < textList.Length)
        {
            timer += Time.deltaTime;
            if (timer <= 5f) return;
            commanderText.text = textList[nextTextIndex];
            nextTextIndex++;
            timer = 0;
        }

        if (textList.Length <= nextTextIndex)
        {
            uIEmergencyWthdrawPresenter.ContainerSetActive(true);
            nextTextIndex++;
        }
    }

    // private void LastContact()
    // {
    //     throw new NotImplementedException();
    // }

    // private void FirstContact()
    // {
    //     timeSinceLastText += Time.deltaTime;
    //     if (timeSinceLastText > 5f)
    //     {
    //         commanderText.text = textList[currentTextIndex];
    //         currentTextIndex++;
    //         timeSinceLastText = 0f;
    //         if (currentTextIndex >= 9)
    //         {
    //             currentState = CommanderTextState.DestroyingWall;
    //         }
    //     }
    // }
}
