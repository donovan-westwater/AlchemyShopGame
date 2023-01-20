using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test Inventory item data")]
public class ScriptibleTest : ScriptableObject
{
    public int type = 0;
    public string name = "ingredient name";
    public Sprite icon;
    public GameObject prefab;
}
