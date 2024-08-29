using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMono : MonoBehaviour
{
    public Action<bool> OnActiveUi;
    public virtual void OnActiveTrue()
    {
        OnActiveUi?.Invoke(true);
    }

    public virtual void OnActiveFalse()
    {
        OnActiveUi?.Invoke(false); 
    }
}
