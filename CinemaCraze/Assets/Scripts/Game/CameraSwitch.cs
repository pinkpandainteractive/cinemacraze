using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera cameraMain;
    public Camera cameraProduct;

    public bool isCameraMainActive = true;

    private void Start()
    {
        // * Aktiviere die erste Kamera und deaktiviere die zweite Kamera zu Beginn
        cameraMain.enabled = true;
        cameraProduct.enabled = false;
    }

    public void Toggle()
    {
        // * Wechsle zwischen den Kameras
        isCameraMainActive = !isCameraMainActive;

        // * Aktiviere/Deaktiviere die Kameras entsprechend
        cameraMain.enabled = isCameraMainActive;
        cameraProduct.enabled = !isCameraMainActive;

    }
}
