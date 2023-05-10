using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{

    public Text selectedObjectText;
    public Material matl_popcorn;
    public Material matl_nacho;
    public Material matl_cash;
    public Material matl_selected;

    public Button btn_delete;

    //private List<GameObject> selectedObjectsList = new List<GameObject>();
    string saveOldText = "";
    public List<string> listSelectedObjects = new List<string>();
    public Dictionary<GameObject, int> selectedObjects = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        //Button btn = btn_delete.GetComponent<Button>();
        //btn.onClick.AddListener(delete);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Define clickedObject
                GameObject clickedObject = hit.collider.gameObject;

                //Allows only the modification of objects with this name
                if (clickedObject.name.Equals("Popcorn") || clickedObject.name.Equals("Nacho") ||
                    clickedObject.name.Equals("Cash") || clickedObject.name.Equals("Customer"))
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


                    if (clickedObject != null && saveOldText != null)
                    {
                        // Deselect previously selected object
                        if (saveOldText.Equals("Popcorn"))
                        {
                            GameObject.Find("Popcorn").GetComponent<Renderer>().material = matl_popcorn;

                        }

                        if (saveOldText.Equals("Nacho"))
                        {
                            GameObject.Find("Nacho").GetComponent<Renderer>().material = matl_nacho;

                        }

                        if (saveOldText.Equals("Cash"))
                        {
                            GameObject.Find("Cash").GetComponent<Renderer>().material = matl_cash;

                        }

                        if (saveOldText.Equals("Customer"))
                        {
                            GameObject.Find("Customer").GetComponent<Renderer>().material.color = Color.white;

                        }
                    }


                    clickedObject.GetComponent<Renderer>().material = matl_selected;
                    saveOldText = clickedObject.name;
                }
                else
                {
                    //If you missclick

                    if (clickedObject != null && saveOldText != null)
                    {
                        // Deselect previously selected object
                        if (saveOldText.Equals("Popcorn"))
                        {
                            GameObject.Find("Popcorn").GetComponent<Renderer>().material = matl_popcorn;

                        }

                        if (saveOldText.Equals("Nacho"))
                        {
                            GameObject.Find("Nacho").GetComponent<Renderer>().material = matl_nacho;

                        }

                        if (saveOldText.Equals("Cash"))
                        {
                            GameObject.Find("Cash").GetComponent<Renderer>().material = matl_cash;

                        }

                        if (saveOldText.Equals("Customer"))
                        {
                            GameObject.Find("Customer").GetComponent<Renderer>().material.color = Color.white;

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
                        Dictionary<GameObject, int> saveObjects = new Dictionary<GameObject, int>();
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
                        Dictionary<GameObject, int> saveObjects = new Dictionary<GameObject, int>();
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

