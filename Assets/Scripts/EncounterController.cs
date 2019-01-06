using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour {

    #region Singleton
    //Instancira sam sebe i održava se na životu kroz sve scene
    public static EncounterController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("More then one instance of EncounterController found!");
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject battleScreen;
    public Enemy[] enemySprites;

    private bool fightInProgress;

    private Image displayEnemy;
    private Text textLog;
    private Text enemyHP;
    private Text playerHP;
    private Enemy enemy;

    private Button attackButton;
    private Button defendButton;
    private Button fleeButton;
    private Button continueButton;

    private void Start()
    {
        fightInProgress = false;
        displayEnemy = battleScreen.transform.Find("DisplayEnemy").GetComponent<Image>();
        displayEnemy.preserveAspect = true;
        textLog = battleScreen.transform.Find("TextLog").GetComponent<Text>();
        enemyHP = battleScreen.transform.Find("EnemyHP").GetComponent<Text>();
        playerHP = battleScreen.transform.Find("PlayerHP").GetComponent<Text>();

        attackButton = battleScreen.transform.Find("AttackButton").GetComponent<Button>();
        defendButton = battleScreen.transform.Find("DefendButton").GetComponent<Button>();
        fleeButton = battleScreen.transform.Find("FleeButton").GetComponent<Button>();
        continueButton = battleScreen.transform.Find("ContinueButton").GetComponent<Button>();

        enemy = ScriptableObject.CreateInstance<Enemy>();
    }

    public bool RollForBattle() {

        if (Random.Range(1, 100) < 20)
        {
            Debug.Log("Battle!");
            fightInProgress = true;
            InitiateBattle();
        }
        return fightInProgress;
    }

    public void InitiateBattle() {
        int roll = Random.Range(0, enemySprites.Length);

        enemy.enemyName = enemySprites[roll].enemyName;
        enemy.enemySprite = enemySprites[roll].enemySprite;
        enemy.hp = enemySprites[roll].hp;

        displayEnemy.sprite = enemy.enemySprite;
        playerHP.text = PlayerStats.instance.GetPlayerName() + "s hp: " + PlayerStats.instance.GetPlayerHp().ToString();
        enemyHP.text = enemy.enemyName + " hp: " + enemy.hp;
        textLog.text = enemy.enemyName + " appears before you!\n";

        attackButton.gameObject.SetActive(true);
        defendButton.gameObject.SetActive(true);
        fleeButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);

        battleScreen.SetActive(true);
    }

    public void FinishBattle() {
        attackButton.gameObject.SetActive(false);
        defendButton.gameObject.SetActive(false);
        fleeButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }


    public void StatusCheck() {
        if (enemy.hp < 1) {
            textLog.text = "You won!";
            FinishBattle();
        }

        if (PlayerStats.instance.GetPlayerHp()<1) {
            textLog.text = "You died!";
            FinishBattle();
        }
    }

    public void EnemyAction() {
        if (enemy.hp > 1)
        {
            PlayerStats.instance.SetPlayerHp(-5);
            textLog.text += "\n" + enemy.enemyName + " hits you for 5 DMG";
            playerHP.text = PlayerStats.instance.GetPlayerName() + "s hp: " + PlayerStats.instance.GetPlayerHp().ToString();
        }
    }


    //Player actions
    public void ActionAttack()
    {
        enemy.hp -= 5;

        textLog.text = "You hit " + enemy.enemyName + " for 5 DMG.";
        enemyHP.text = enemy.enemyName + " hp: " + enemy.hp;
        StatusCheck();
        EnemyAction();
        StatusCheck();
    }

    public void ActionDefend()
    {
        textLog.text = "You attempt to defend.";
        EnemyAction();
        StatusCheck();
    }

    public void ActionFlee()
    {
        textLog.text = "You fled from battle.";
        FinishBattle();
    }

    public void ActionContinue() {
        if (PlayerStats.instance.GetPlayerHp() < 1) {
            PlayerStats.instance.PlayerIsDead();
        }
        fightInProgress = false;
        battleScreen.SetActive(false);
    }


    //unlock movement after battle is over
    public bool AreWeFighting() {
        return fightInProgress;
    }

}
