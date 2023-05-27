using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    public Camera CameraMain;
    public Camera CameraProduct;
    public CameraSwitch CameraSwitch;

    public Text selectedObjectText;
    public Material matl_popcorn;
    public Material matl_nacho;
    public Material matl_cash;
    public Material matl_selected;
    public Material matl_colorPalette;
    public Button btn_delete;
    public MainMenu menuStatus;
    
    
    public List<string> listSelectedObjects = new ();
    public Dictionary<GameObject, int> selectedObjects = new ();

    string _saveOldText = "";

    // Start is called before the first frame update
    void Start()
    {
        Button btn = btn_delete.GetComponent<Button>();
        btn.onClick.AddListener(Delete);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            if (CameraSwitch.isCameraMainActive == true)
            {
                ray = CameraMain.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = CameraProduct.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Define clickedObject
                GameObject clickedObject = hit.collider.gameObject;

                //Allows only the modification of objects with this name
                if (clickedObject.name.Equals("Popcorn") || clickedObject.name.Equals("Nacho") || clickedObject.name.Equals("Soda"))
                {

                    if (selectedObjects.ContainsKey(clickedObject))
                    {
                        selectedObjects[clickedObject]++;
                    }
                    else
                    {
                        selectedObjects.Add(clickedObject, 1);
                        

                    }

                    UpdateObjectNameText();


                    if (clickedObject != null && _saveOldText != null)
                    {
                        // Deselect previously selected object
                        if (_saveOldText.Equals("Popcorn"))
                        {
                            GameObject.Find("Popcorn").GetComponent<Renderer>().material = matl_colorPalette;
                        }

                        if (_saveOldText.Equals("Nacho"))
                        {
                            GameObject.Find("Nacho").GetComponent<Renderer>().material = matl_nacho;
                        }

                        if (_saveOldText.Equals("Soda"))
                        {
                            GameObject.Find("Soda").GetComponent<Renderer>().material = matl_colorPalette;
                        }
                    }


                    clickedObject.GetComponent<Renderer>().material = matl_selected;
                    _saveOldText = clickedObject.name;
                }
                else
                {
                    //If you missclick

                    if (clickedObject != null && _saveOldText != null)
                    {
                        // Deselect previously selected object
                        if (_saveOldText.Equals("Popcorn"))
                        {
                            GameObject.Find("Popcorn").GetComponent<Renderer>().material = matl_colorPalette;
                        }

                        if (_saveOldText.Equals("Nacho"))
                        {
                            GameObject.Find("Nacho").GetComponent<Renderer>().material = matl_nacho;
                        }

                        if (_saveOldText.Equals("Soda"))
                        {
                            GameObject.Find("Soda").GetComponent<Renderer>().material = matl_colorPalette;
                        }
                    }
                }
                
                if (clickedObject.name.Equals("DeleteNacho") && selectedObjects.ContainsKey(GameObject.Find("Nacho"))
                    && selectedObjects[GameObject.Find("Nacho")] > 0)
                {
                    
                    selectedObjects[GameObject.Find("Nacho")]--;
                    if (selectedObjects[GameObject.Find("Nacho")] == 0)
                    {
                        selectedObjectText.text = " ";
                        Dictionary<GameObject, int> saveObjects = new ();

                        foreach (KeyValuePair<GameObject, int> pair in selectedObjects)
                        {
                            if (!pair.Key.name.Equals("Nacho"))
                            {
                                selectedObjectText.text += pair.Key.name + " x" + pair.Value + " ";
                                saveObjects.Add(pair.Key, pair.Value);
                            }


                            if (pair.Key != selectedObjects.Keys.Last())
                            {
                                selectedObjectText.text += ", ";
                            }
                        }
                        selectedObjects = saveObjects;
                    }
                    UpdateObjectNameText();
                }

                if (clickedObject.name.Equals("DeletePopcorn") && selectedObjects.ContainsKey(GameObject.Find("Popcorn")) 
                    && selectedObjects[GameObject.Find("Popcorn")] > 0)
                {

                    selectedObjects[GameObject.Find("Popcorn")]--;
                    if (selectedObjects[GameObject.Find("Popcorn")] == 0)
                    {
                        selectedObjectText.text = " ";
                        Dictionary<GameObject, int> saveObjects = new ();

                        foreach (KeyValuePair<GameObject, int> pair in selectedObjects)
                        {
                            if (!pair.Key.name.Equals("Popcorn"))
                            {
                                selectedObjectText.text += pair.Key.name + " x" + pair.Value + " ";
                                saveObjects.Add(pair.Key, pair.Value);
                            }
                            

                            if (pair.Key != selectedObjects.Keys.Last())
                            {
                                selectedObjectText.text += ", ";
                            }
                        }
                            selectedObjects = saveObjects;
                    }
                    UpdateObjectNameText();
                }

                if (clickedObject.name.Equals("DeleteSoda") && selectedObjects.ContainsKey(GameObject.Find("Soda"))
                    && selectedObjects[GameObject.Find("Soda")] > 0)
                {

                    selectedObjects[GameObject.Find("Soda")]--;
                    if (selectedObjects[GameObject.Find("Soda")] == 0)
                    {
                        selectedObjectText.text = " ";
                        Dictionary<GameObject, int> saveObjects = new();

                        foreach (KeyValuePair<GameObject, int> pair in selectedObjects)
                        {
                            if (!pair.Key.name.Equals("Soda"))
                            {
                                selectedObjectText.text += pair.Key.name + " x" + pair.Value + " ";
                                saveObjects.Add(pair.Key, pair.Value);
                            }


                            if (pair.Key != selectedObjects.Keys.Last())
                            {
                                selectedObjectText.text += ", ";
                            }
                        }
                        selectedObjects = saveObjects;
                    }
                    UpdateObjectNameText();
                }

            }
        }
    }

    public void Delete()
    {
        selectedObjectText.text = "";
        selectedObjects = new Dictionary<GameObject, int>();
        listSelectedObjects.Clear();
    }
    private void UpdateObjectNameText()
    {
        selectedObjectText.text = " ";
        listSelectedObjects = new List<string>();
        foreach (KeyValuePair<GameObject, int> pair in selectedObjects)
        {
            selectedObjectText.text += pair.Key.name + " x" + pair.Value + " ";
            listSelectedObjects.Add(pair.Key.name + " x" + pair.Value);
            listSelectedObjects.Sort();
            
            if (pair.Key != selectedObjects.Keys.Last())
            {
                selectedObjectText.text += ", ";
            }
        }
    }
}

