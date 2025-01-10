using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameService : IMinigameService
{
    public IngredientType[] CurrentRecipe { get; set; }
    public bool IsInMinigame { get; set; }
    public event EventHandler<MinigameType> OnMinigameRequested;
    public event EventHandler<IngredientType> IngredientAdded;

    public event EventHandler<MinigamePointsUpdatedEventArgs> OnMinigamePointsUpdated;
    public event EventHandler<MinigameType> OnMinigameOver;
    public void RequestMinigame(MinigameType minigameType) => OnMinigameRequested?.Invoke(this, minigameType);
    public void OnIngredientAdded(IngredientType ingredientType) => IngredientAdded?.Invoke(this, ingredientType);

    public void SetInMinigame(bool isInMinigame) => IsInMinigame = isInMinigame;
    public void OnPointsUpdated(MinigamePointsUpdatedEventArgs args) => OnMinigamePointsUpdated?.Invoke(this, args);
    public void OnGameOver(MinigameType minigameType) => OnMinigameOver?.Invoke(this, minigameType);
}
