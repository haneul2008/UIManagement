using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library.UIManagement;

public class UITester : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.ActiveUI(UiType.Image1, true);
        UIManagementUtillity.SetUiPos(UiType.Image1, UIPos.LeftTop, new Vector2(40f, -40f));
    }
}
