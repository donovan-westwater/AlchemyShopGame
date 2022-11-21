using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CabinetInventory : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public static CabinetInventory instance = null;
    public static CabnetController currentInv = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    //Needs to be attached to the panel, create a system to send info to UI and have script manage transfer on that end
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.tag == "Ingredient") return;
        Ingredient.inventory = this.gameObject;
        Debug.Log("Cabinet ENTER " + Ingredient.inventory.name+" "+ eventData.pointerEnter.gameObject.tag);
    }
    //Check if hover over inventory and remove it [Might be good to move to inventory section?]
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Cabient EXIT" + Ingredient.inventory.name);
        //Ingredient.inventory = null;
    }
    // Update is called once per frame
    void Update()
    {
        //Update the inventory based on which cabinet your are near
    }
}
