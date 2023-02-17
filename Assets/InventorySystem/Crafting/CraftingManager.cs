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
    public bool outputEmpty = true;
    [HideInInspector]
    public bool isCrafting = false;
    public bool controlRange = false;
    // Start is called before the first frame update
    void Start()
    {
        inventoryBackend.gameObject.SetActive(false);
        a.data = null;
        b.data = null;
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
            bool OrderA = r.inputA == a.data && a.stackSize == r.aAmount && r.inputB == b.data && b.stackSize == r.bAmount;
            bool OrderB = r.inputA == b.data && b.stackSize == r.aAmount && r.inputB == a.data && a.stackSize == r.bAmount;
            if (OrderA || OrderB)
            {
                //a.RemoveFromStack();
                //b.RemoveFromStack();
               
                if (outputEmpty)
                {
                    outputEmpty = false;
                    output = new InventoryItem(r.output);
                }
                else
                {
                    output.AddToStack();
                }
                inventoryBackend.Remove(a.data);
                inventoryBackend.Remove(b.data);

                if (a.stackSize < 1) a.data = null;
                if (b.stackSize < 1) b.data = null;
                //ui.CraftingDraw();
                //inventoryBackend.Add(r.output);
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
