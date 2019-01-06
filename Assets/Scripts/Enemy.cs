using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Encounter/Enemy")]

public class Enemy : ScriptableObject
{
    public string enemyName;
    public int hp;
    public Sprite enemySprite;

}
