using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseInputHandler : MonoBehaviour
{
    const int LEFT_CLICK = 0;
    const int RIGHT_CLICK = 1;
    const int MIDDLE_CLICK = 2;

    public Inventory inventory;
    public ProductManager productManager;
    public MachineManager machineManager;
    public UpgradesManager upgradesManager;

    public Camera mainCamera;
    public Camera productCamera;
    Camera activeCamera;
    int layerMask;
    Ray ray;
    RaycastHit hit;
    GameObject clickedObject;

    void Start()
    {
        // * Ignores WaitingZone
        layerMask = ~LayerMask.GetMask("WaitingZone");
        
    }
    void Update()
    {
        // ! Bitte alle MouseEvents hier eintragen

        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if(raycastResults.Count > 0)
                foreach(var ray in raycastResults)
                    HandleLeftClick(ray.gameObject);
            else
                if (Physics.Raycast(computeRay(), out hit))
                    HandleLeftClick(hit.collider.gameObject);
        }
        else if (Input.GetMouseButtonDown(RIGHT_CLICK))
        {
            // * Checks if the mouse is over an object
            if (Physics.Raycast(computeRay(), out hit, Mathf.Infinity, layerMask))
                HandleRightClick(hit.collider.gameObject);
        }
        else if (Input.GetMouseButtonDown(MIDDLE_CLICK))
        {
            // * Checks if the mouse is over an object
            if (Physics.Raycast(computeRay(), out hit, Mathf.Infinity, layerMask))
                HandleMiddleClick(hit.collider.gameObject);
        }
    }

    Ray computeRay()
    {
        // * Computes the ray depending on the active camera
        activeCamera = mainCamera.enabled ? mainCamera : productCamera;
        return activeCamera.ScreenPointToRay(Input.mousePosition);
    }

    void HandleLeftClick(GameObject obj)
    {
        if (obj == null) return;

        string tag = obj.tag;
        
        if (tag.Equals("Customer"))
        {
            CustomerLogic customerLogic = obj.GetComponent<CustomerLogic>();
            customerLogic.HandInOrder();
        }
        else if (tag.Equals("Popcorn"))
        {
            if (inventory.HasSlotAvailable())
                productManager.StartTimer(tag, obj);
        }
        else if (tag.Equals("Nachos"))
        {
            if (inventory.HasSlotAvailable())
                productManager.StartTimer(tag, obj);
        }
        else if (tag.Equals("Soda"))
        {
            if (inventory.HasSlotAvailable())
                productManager.StartTimer(tag, obj);
        }
        else if (tag.Equals("Machine"))
        {
            machineManager.HandleBuyScreen(obj);
        }
        else if (!tag.Equals("UI_Upgrades"))
        {
           // upgradesManager.CloseUpgradesMenu();
        }
        
        
    }

    void HandleRightClick(GameObject obj)
    {
        if (obj == null) return;

        string tag = obj.tag;

        if (tag.Equals("Popcorn"))
        {
            inventory.RemovePopcorn(1);
        }
        else if (tag.Equals("Nachos"))
        {
            inventory.RemoveNachos(1);
        }
        else if (tag.Equals("Soda"))
        {
            inventory.RemoveSoda(1);
        }
    }

    void HandleMiddleClick(GameObject obj)
    {
        if (obj == null) return;
    }
}