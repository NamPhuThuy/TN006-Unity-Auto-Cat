using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasShop : CanvasBase
{
    private TMP_Text ItemName;
    private TMP_Text Price;

    public GameObject[] itemLists;  // Assign each item list panel in the Inspector

    // This method is called when a category button is clicked
    public void ShowItemList(int index)
    {
        // Hide all item lists
        foreach (GameObject itemList in itemLists)
        {
            itemList.SetActive(false);
        }

        // Show the selected item list
        if (index >= 0 && index < itemLists.Length)
        {
            itemLists[index].SetActive(true);
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show(object data = null)
    {
        base.Show(data);
    }
}
