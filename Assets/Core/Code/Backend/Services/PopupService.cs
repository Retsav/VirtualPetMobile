using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupService : IPopupService
{
    public event EventHandler CloseMinigamePopupEvent;
    public event EventHandler OpenMinigamePopupEvent;
    
    public void OnCloseMinigamePopupEvent() => CloseMinigamePopupEvent?.Invoke(this, EventArgs.Empty);
    public void OnOpenMinigamePopupEvent() => OpenMinigamePopupEvent?.Invoke(this, EventArgs.Empty);
}
