using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGarage : CanvasBase
{
    [SerializeField] private GameObject _itemUpgradeTemplate;
    [SerializeField] private GameObject _content;
    public override void Show(object data = null)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
    }
}
