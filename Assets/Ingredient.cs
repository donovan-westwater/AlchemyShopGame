using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Ingredient : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler,IUpdateSelectedHandler
{
    public static GameObject inventory = null;
    private GameObject currentInv = null;
    private GameObject selectedObject = null;
    public int type = 0; //0 Salt, 1 Sulfer, 1 Mercury
    public void Init(int t,GameObject i)
    {
        type = t;
        currentInv = i;
    }
    //Use the Event System to create a drag and drop system for ingredients
    //ingredient script should handle their own transfer to inventories (player and cabient)
    //Select the ingredient to be moved
    public void OnPointerDown(PointerEventData eventData)
    {
        selectedObject = eventData.selectedObject;
    }
    //Release the ingredient and ether return to orginal inventory
    //Or place in currently hovered over inventory
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("TEST Release");
        //Needs to return to previous position. Currently doesn't do that
        Debug.Log("Current Inventory: "+inventory.gameObject.name);
        if (inventory != null)
        {
            if (currentInv == null || currentInv.name != inventory.name) {
                if (inventory.CompareTag("PlayerInventory"))
                {
                    PlayerInventory.instance.AddIngredient(this.type);
                    CabinetInventory.currentInv.RemoveIngredient(this.gameObject);
                }
                else
                {
                    CabinetInventory.currentInv.AddIngredient(this.type,true);
                    PlayerInventory.instance.RemoveIngredient(this.gameObject);
                    
                }
                currentInv = inventory;
            }
            selectedObject.gameObject.transform.SetParent(inventory.transform);

        }
        selectedObject = null;
    }
    
    //Update location of the sprite
    public void OnUpdateSelected(BaseEventData eventData)
    {
        if(selectedObject != null) { 
            selectedObject.transform.position = new Vector3(eventData.currentInputModule.input.mousePosition.x,
            eventData.currentInputModule.input.mousePosition.y,
            0);
        }
    }
    
}
