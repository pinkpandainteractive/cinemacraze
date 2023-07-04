using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradesManager : MonoBehaviour
{
    public Button btn_upgrades;
    public Button btn_close;

    public GameObject upgradesMenu;
    // Start is called before the first frame update
    void Start()
    {
        btn_upgrades.onClick.AddListener(OpenUpgradesMenu);
        btn_close.onClick.AddListener(CloseUpgradesMenu);
    }
  
    public void OpenUpgradesMenu()
    {
        upgradesMenu.SetActive(true);
        btn_upgrades.gameObject.SetActive(false);
    }
    public void CloseUpgradesMenu()
    {
        upgradesMenu.SetActive(false);
        btn_upgrades.gameObject.SetActive(true);
    }
}
