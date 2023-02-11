using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public InventorySystem inventoryBackend;
    public CraftingUIManager ui;
    public Recipe[] recipes;
    [HideInInspector]
    public InventoryItem a;
    [HideInInspector]
    public InventoryItem b;
    [HideInInspector]
    public InventoryItem output;
    [HideInInspector]
    public bool isCrafting = false;
    public bool controlRange = false;
    // Start is called before the first frame update
    void Start()
    {
        inventoryBackend.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlRange && Input.GetKeyDown(KeyCode.E))
        {
            isCrafting = !isCrafting;
        }
        if (isCrafting) ui.gameObject.SetActive(true);
        else ui.gameObject.SetActive(false);
        //Remove extra items in inventory
        if (inventoryBackend.size > 2)
        {
            foreach (InventoryItem c in inventoryBackend.inventory)
            {
                if (c != a && c != b && c != output)
                {
                    inventoryBackend.Remove(c.data);
                    break;
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            AttemptCrafting();
        }
    }
    void AttemptCrafting()
    {
        if (a == null || b == null) return;
        foreach(Recipe r in recipes)
        {
            if(r.inputA == a.data &&a.stackSize == r.aAmount && r.inputB == b.data && b.stackSize == r.bAmount)
            {
                output = new InventoryItem(r.output);
                inventoryBackend.Add(r.output);
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        controlRange = true;
    }
    private void OnTriggerStay(Collider other)
    {
        controlRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isCrafting = false;
        controlRange = false;
    }
}
