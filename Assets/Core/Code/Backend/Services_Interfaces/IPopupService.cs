using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopupService
{
    public event EventHandler CloseMinigamePopupEvent;
    public event EventHandler OpenMinigamePopupEvent;
    public void OnCloseMinigamePopupEvent();
    public void OnOpenMinigamePopupEvent();
    
}
