using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Debug.LogWarning("More then one instance of Inventory found!");
            Destroy(gameObject);
        }
    }

    #endregion

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public bool Contains(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        else {
            return false;
        }
    }
    
}
