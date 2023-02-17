using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCrafting : DisplayInventory
{
    CraftingManager system;
    bool isOutputSlot = false;
    InventoryItem slottedItem;
    public void Set(CraftingManager m,InventoryItem i,bool b)
    {
        system = m;
        isOutputSlot = b;
        slottedItem = i;
        
    }
    public void TransferClick()
    {
        if (isOutputSlot)
        {
            if (system.outputEmpty) return;
            slottedItem.RemoveFromStack();
            if (slottedItem.stackSize < 1)
            {
                system.outputEmpty = true;
            }
            system.inventoryBackend.playerInv.GetComponent<InventorySystem>().Add(slottedItem.data);
            system.inventoryBackend.Remove(slottedItem.data);
            system.output = slottedItem;
        }
        else
        {
            ScriptibleIngredient ing = slottedItem.data;
            system.inventoryBackend.Remove(slottedItem.data);
            if (slottedItem.data == system.a.data)
            { 
                if (slottedItem.stackSize == 0) system.a.data = null;
                else system.a = slottedItem;
            }
            else
            {
                if (slottedItem.stackSize == 0) system.b.data = null;
                else system.b = slottedItem;
            }
            system.inventoryBackend.playerInv.GetComponent<InventorySystem>().Add(ing);
        }
        
    }
}
