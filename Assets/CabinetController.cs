using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    [SerializeField]
    InventorySystem assoiatedUI;

    // Start is called before the first frame update
    void Start()
    {
        assoiatedUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can access!");
        assoiatedUI.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        assoiatedUI.gameObject.SetActive(false);
    }
}
