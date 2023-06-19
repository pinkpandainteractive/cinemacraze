using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    const int LEFT_CLICK = 0;
    const int RIGHT_CLICK = 1;
    const int MIDDLE_CLICK = 2;

    public CameraSwitch cameraSwitch;
    public Customer customer;
    public Inventory inventory;
    public MachineManager machineManager;

    Camera activeCamera;
    int layerMask;
    Ray ray;
    RaycastHit hit;
    GameObject clickedObject;

    void Start()
    {
        //  Ignores WaitingZone
        layerMask = ~LayerMask.GetMask("WaitingZone");
        
    }
    void Update()
    {
        // ! Bitte alle MouseEvents hier eintragen

        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            // Checks if the mouse is over an object
            if (Physics.Raycast(computeRay(), out hit))
            {
                clickedObject = hit.collider.gameObject;
                HandleLeftClick(clickedObject);
            }
        }
        else if (Input.GetMouseButtonDown(RIGHT_CLICK))
        {
            // Checks if the mouse is over an object
            if (Physics.Raycast(computeRay(), out hit, Mathf.Infinity, layerMask))
            {
                clickedObject = hit.collider.gameObject;
                HandleRightClick(clickedObject);
            }
        }
        else if (Input.GetMouseButtonDown(MIDDLE_CLICK))
        {
            // Checks if the mouse is over an object
            if (Physics.Raycast(computeRay(), out hit, Mathf.Infinity, layerMask))
            {
                clickedObject = hit.collider.gameObject;
                HandleMiddleClick(clickedObject);
            }
        }
    }

    Ray computeRay()
    {
        // * Computes the ray depending on the active camera
        activeCamera = cameraSwitch.isCameraMainActive ? cameraSwitch.cameraMain : cameraSwitch.cameraProduct;
        return activeCamera.ScreenPointToRay(Input.mousePosition);
    }

    void HandleLeftClick(GameObject obj)
    {
        
        if (obj == null) return;

        
        //Debug.Log(obj.tag);
        //Debug.Log(obj.name);
        
        string tag = obj.tag;
        
       
        
        if (tag.Equals("Customer"))
        {
            Customer customer = obj.GetComponent<Customer>();
            customer.HandInOrder();
        }
        else if (tag.Equals("Popcorn"))
        {
            inventory.AddPopcorn(1);
        }
        else if (tag.Equals("Nachos"))
        {
            inventory.AddNachos(1);
        }
        else if (tag.Equals("Soda"))
        {
            inventory.AddSoda(1);
        }
        else if (tag.Equals("Machine"))
        {
            machineManager.HandleBuyScreen(obj);
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