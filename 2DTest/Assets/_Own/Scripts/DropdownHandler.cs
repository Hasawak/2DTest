using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public Button joinButton;
    public Dropdown dropdown;

    public bool colorChosen;
    public string circlePrefab;
    public string selectedColor;

    public List<string> items;
    public List<bool> optionsChosen;

    //public Text TextBox;
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        items = new List<string>
        {
            "Wähle deine Farbe",
            "Blau",
            "Dunkelgrün",
            "Gelb",
            "Grün",
            "Hellblau",
            "Lila",
            "Orange",
            "Pink",
            "Rot",
            "Türkis"
        };

        // Fill dropdown with items
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
            optionsChosen.Add(false);
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        if(index == 0)
        {
            colorChosen = false;
        }
        else
        {
            circlePrefab = "PlayerCircle" + dropdown.options[index].text;
            selectedColor = dropdown.options[index].text;
            colorChosen = true;
        }
    }
}
