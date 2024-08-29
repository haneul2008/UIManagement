using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UiManagement/UiElement")]
public class UiElementSO : ScriptableObject
{
    public UIMono ui;
    public string key => ui.gameObject.name;
}
