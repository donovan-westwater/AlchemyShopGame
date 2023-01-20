using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Follwing this tutorial: https://www.youtube.com/watch?v=SGz3sbZkfkg

[Serializable]
public class InventoryItem
{
    public ScriptibleTest data;
    public int stackSize;
    public InventoryItem(ScriptibleTest source)
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
public class InventorySystemTest : MonoBehaviour
{

    private Dictionary<ScriptibleTest, InventoryItem> itemDict;
    public ScriptibleTest test;
    public List<InventoryItem> inventory;
    public GameObject playerInv;
    public static InventorySystemTest instance;
    public delegate void OnInventoryChangedEvent();
    public event OnInventoryChangedEvent onInventoryChangedEvent;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        inventory = new List<InventoryItem>();
        itemDict = new Dictionary<ScriptibleTest, InventoryItem>();
    }

    public void Add(ScriptibleTest referenceData)
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

    public void Remove(ScriptibleTest referenceData)
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
