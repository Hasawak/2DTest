using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCustomization : MonoBehaviour
{
    public GameObject dropdown = null;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        DisableToggle();
    }

    public void DisableToggle(){
        if (toggle != null && toggle.name == "Item 0: Wähle deine Farbe")
        {
            //toggle.interactable = false;
            GameObject.Find(toggle.name).SetActive(false);
        }
    }
}
