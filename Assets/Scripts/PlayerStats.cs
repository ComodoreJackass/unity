using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    #region Singleton
    //Instancira sam sebe i održava se na životu kroz sve scene
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("More then one instance of PlayerStats found!");
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject enterName;

    private string playerName;
    private int playerHp;

    private void Start()
    {
        playerHp = 20;
        playerName = "";
    }

    public string GetPlayerName() {
        return playerName;
    }

    public int GetPlayerHp() {
        return playerHp;
    }

    public void SetPlayerName(InputField name) {
        playerName = name.text;
        Debug.Log(playerName);
    }

    public void Proceed()
    {
        if (playerName.Length > 0)
        {
            Debug.Log(playerName);
            enterName.SetActive(false);
        }
    }

    public void SetPlayerHp(int hp) {
        playerHp += hp;
    }

    public void PlayerIsDead() {
        Destroy(Inventory.instance.gameObject);
        Destroy(EncounterController.instance.gameObject);
        Destroy(PersistCanvas.instance.gameObject);
        Destroy(instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

}
