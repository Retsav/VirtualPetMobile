using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigameService
{
    public IngredientType[] CurrentRecipe { get; set; }
    public bool IsInMinigame { get; set; } 
    public event EventHandler<MinigameType> OnMinigameRequested;
    public event EventHandler<IngredientType> IngredientAdded;
    public event EventHandler<MinigamePointsUpdatedEventArgs> OnMinigamePointsUpdated;
    public event EventHandler<MinigameType> OnMinigameOver;
    
    public void RequestMinigame(MinigameType minigameType);
    public void OnIngredientAdded(IngredientType ingredientType);
    
    public void SetInMinigame(bool isInMinigame);
    public bool GetIsInMinigame() => IsInMinigame;
    public void OnPointsUpdated(MinigamePointsUpdatedEventArgs args);
    public void OnGameOver(MinigameType minigameType);
}

public class MinigamePointsUpdatedEventArgs : EventArgs {
    public MinigameType MinigameType;
    public int Points;
    public MinigamePointsUpdatedEventArgs(MinigameType minigameType, int points)
    {
        MinigameType = minigameType;
        Points = points;
    }
}

public enum MinigameType
{
    None,
    Cleaning,
    ThreeCups,
    Cauldron
}
