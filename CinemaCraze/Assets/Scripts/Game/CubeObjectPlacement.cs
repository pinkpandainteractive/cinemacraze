using UnityEngine;

public class CubeObjectPlacement : MonoBehaviour
{
    public GameObject objectPrefab; // Das Prefab deiner Objects
    private Transform cubeTransform; // Der Transform des W�rfels

    private void Start()
    {
        cubeTransform = transform; // Den Transform des W�rfels erhalten
    }

    private void Update()
    {
        // �berpr�fen, ob die Maustaste geklickt wurde
        if (Input.GetMouseButtonDown(0))
        {
            // �berpr�fen, ob die maximale Anzahl von Objects (9) noch nicht erreicht ist
            if (transform.childCount < 9)
            {
                // Mausposition in der Weltkoordinaten umwandeln
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Ein neues Object erstellen
                GameObject newObject = Instantiate(objectPrefab, mousePosition, Quaternion.identity);

                // Das neue Object dem W�rfel unterordnen
                newObject.transform.parent = cubeTransform;
            }
        }
    }
}
