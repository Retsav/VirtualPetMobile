using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigameService
{
    public bool IsInMinigame { get; set; } 
    public event EventHandler<MinigameType> OnMinigameRequested;
    public void RequestMinigame(MinigameType minigameType);
    public void SetInMinigame(bool isInMinigame);
    public bool GetIsInMinigame() => IsInMinigame;
}

public enum MinigameType
{
    None,
    Cleaning
}
