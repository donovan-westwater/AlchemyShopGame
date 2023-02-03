using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test Inventory item data")]
public class ScriptibleIngredient : ScriptableObject
{
    public enum Type
    {
        SULFER = 0,
        MECURERY = 1,
        SALTPETER = 2
    }
    public Type type; //0 for sulfer, 1 for Me
    public string name = "ingredient name";
    public Sprite icon;
    public GameObject prefab;
}
