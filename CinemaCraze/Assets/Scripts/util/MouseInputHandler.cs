using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    const int LEFT_CLICK = 0;
    const int RIGHT_CLICK = 1;
    const int MIDDLE_CLICK = 2;

    public CameraSwitch cameraSwitch;
    public Customer customer;
    public Inventory inventory;
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
            if (Physics.Raycast(computeRay(), out hit, Mathf.Infinity, layerMask))
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

        if (obj.name.Contains("Customer"))
        {
            Customer customer = obj.GetComponent<Customer>();
            customer.HandInOrder();
        }
        else if (obj.name.Contains("Popcorn"))
        {
            inventory.AddPopcorn(1);
        }
        else if (obj.name.Contains("Nacho"))
        {
            inventory.AddNachos(1);
        }
        else if (obj.name.Contains("Soda"))
        {
            inventory.AddSoda(1);
        }

    }

    void HandleRightClick(GameObject obj)
    {
        if (obj == null) return;

        if (obj.name.Contains("Popcorn"))
        {
            inventory.RemovePopcorn(1);
        }
        else if (obj.name.Contains("Nacho"))
        {
            inventory.RemoveNachos(1);
        }
        else if (obj.name.Contains("Soda"))
        {
            inventory.RemoveSoda(1);
        }
    }

    void HandleMiddleClick(GameObject obj)
    {
        if (obj == null) return;

    }
}