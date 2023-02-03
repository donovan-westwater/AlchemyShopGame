using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Follwing this tutorial: https://www.youtube.com/watch?v=SGz3sbZkfkg

[Serializable]
public class InventoryItem
{
    public ScriptibleIngredient data;
    public int stackSize;
    public InventoryItem(ScriptibleIngredient source)
    {
        data = source;
        AddToStack();
    }
    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
}
//SHOULD BE A SINGLETON!
public class InventorySystem : MonoBehaviour
{

    private Dictionary<ScriptibleIngredient, InventoryItem> itemDict;
    private int ID = 0;
    public bool isPlayerInv = false;
    public ScriptibleIngredient test;
    public List<InventoryItem> inventory;
    public GameObject playerInv; //currently not used
    public static List<InventorySystem> instanceList; //player reference
    public delegate void OnInventoryChangedEvent();
    public event OnInventoryChangedEvent onInventoryChangedEvent;
    private void Awake()
    {
        if(instanceList == null)
        {
            instanceList = new List<InventorySystem>();
        }
        instanceList.Add(this);
        ID = instanceList.Count;
        if (gameObject.tag.Equals("PlayerInventory")) isPlayerInv = true;
        inventory = new List<InventoryItem>();
        itemDict = new Dictionary<ScriptibleIngredient, InventoryItem>();
    }
    private void OnDestroy()
    {
        if (instanceList != null)
        {
            instanceList.Remove(this);
        }
    }
    
    public void Add(ScriptibleIngredient referenceData)
    {
        if(itemDict.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            itemDict.Add(referenceData, newItem);
        }
        onInventoryChangedEvent();
    }

    public void Remove(ScriptibleIngredient referenceData)
    {
        if(itemDict.TryGetValue(referenceData,out InventoryItem value))
        {
            value.RemoveFromStack();
            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDict.Remove(referenceData);
            }
        }
        onInventoryChangedEvent();
    }
    private void Update()
    {
        if (isPlayerInv) { 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                this.Add(test);
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                this.Remove(test);
            }
        }
    }
    
}
