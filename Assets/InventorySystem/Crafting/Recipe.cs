using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test Recipe data")]
public class Recipe : ScriptableObject
{
    public ScriptibleIngredient inputA;
    public int aAmount = 1;
    public ScriptibleIngredient inputB;
    public int bAmount = 1;
    public ScriptibleIngredient output;
    public int outAmount = 1;
}
