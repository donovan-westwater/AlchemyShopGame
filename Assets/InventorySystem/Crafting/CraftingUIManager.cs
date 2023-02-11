using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUIManager : MonoBehaviour
{
    public CraftingManager backend;
    public GameObject aSlot;
    public GameObject bSlot;
    public GameObject outputSlot;
    public GameObject slotPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        backend.inventoryBackend.onInventoryChangedEvent += CraftingUpdate;
    }

    void CraftingUpdate()
    {
        //Update item slot vars
        if (backend.inventoryBackend.size > 1)
        {
            if (backend.inventoryBackend.inventory[0].stackSize > 1) backend.b = backend.inventoryBackend.inventory[0];
            else backend.b = backend.inventoryBackend.inventory[1];
            if (backend.inventoryBackend.size > 2) backend.output = backend.inventoryBackend.inventory[2];
            
        }
        if (backend.inventoryBackend.size > 0)
        {
            backend.a = backend.inventoryBackend.inventory[0];
        }
        //Clear item slot objects
        if (aSlot.transform.childCount > 0) Destroy(aSlot.transform.GetChild(0).gameObject);
        if (bSlot.transform.childCount > 0) Destroy(bSlot.transform.GetChild(0).gameObject);
        if (outputSlot.transform.childCount > 0) Destroy(outputSlot.transform.GetChild(0).gameObject);
        //Create new item slots
        CraftingDraw();
    }
    void CraftingDraw()
    {
        if (backend.inventoryBackend.size > 0) SetCraftingSlot(backend.a,aSlot);
        if (backend.inventoryBackend.size > 1) SetCraftingSlot(backend.b, bSlot);
        if (backend.inventoryBackend.size > 2) SetCraftingSlot(backend.output, outputSlot);
    }
    void SetCraftingSlot(InventoryItem c,GameObject s)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(s.transform, false);

        DisplayInventory slot = obj.GetComponent<DisplayInventory>();
        slot.Set(c);
        slot.SetInventory(backend.inventoryBackend);
    }
    
}
