using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.LogWarning("More then one instance of Inventory found!");
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject battleScreen;

    private bool fightInProgress;

    private void Start()
    {
        fightInProgress = false;
    }

    public bool RollForBattle() {

        if (Random.Range(1, 100) < 15)
        {
            Debug.Log("Battle!");
            fightInProgress = true;
            battleScreen.SetActive(true);
        }
        return fightInProgress;
    }

    public void InstantWIn()
    {
        fightInProgress = false;
        battleScreen.SetActive(false);
    }

    public bool AreWeFighting() {
        return fightInProgress;
    }

}
