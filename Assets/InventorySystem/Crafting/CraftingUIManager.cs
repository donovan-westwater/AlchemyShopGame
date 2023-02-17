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
        //Update item input slot vars
        //Remove extra items in inventory
        foreach (InventoryItem c in backend.inventoryBackend.inventory)
        {
            if (backend.a.data == null && backend.b.data != c.data)
            {
                backend.a = c;
            }
            else if (backend.b.data == null && backend.a.data != c.data)
            {
                backend.b = c;
            }
            else if (c.data != backend.a.data && c.data != backend.b.data)
            {
                backend.inventoryBackend.Remove(c.data);
                //break;
            }
            else
            {
                if (backend.a.data == c.data) backend.a.stackSize = c.stackSize;
                if (backend.b.data == c.data) backend.b.stackSize = c.stackSize;
            }

        }
       
        //Draw the slots
        CraftingDraw();
    }
    public void CraftingDraw()
    {
        //Clear item slot objects
        if (aSlot.transform.childCount > 0) DestroyImmediate(aSlot.transform.GetChild(0).gameObject);
        if (bSlot.transform.childCount > 0) DestroyImmediate(bSlot.transform.GetChild(0).gameObject);
        if (outputSlot.transform.childCount > 0) DestroyImmediate(outputSlot.transform.GetChild(0).gameObject);
        //create new item slots
        if (backend.a.data != null && backend.a.stackSize > 0) SetCraftingSlot(backend.a,aSlot,false);
        if (backend.b.data != null && backend.b.stackSize > 0) SetCraftingSlot(backend.b, bSlot,false);
        if (backend.outputEmpty == false) SetCraftingSlot(backend.output, outputSlot,true);
    }
    void SetCraftingSlot(InventoryItem c,GameObject s,bool b)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(s.transform, false);
        obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

        DisplayCrafting slot = obj.GetComponent<DisplayCrafting>();
        slot.Set(c);
        slot.Set(backend, c, b);
        slot.SetInventory(backend.inventoryBackend);
    }

    
}
