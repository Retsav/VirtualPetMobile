using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = System.Random;


public class CauldronMinigame : MonoBehaviour
{
    [SerializeField] private float recipeSteps = 3;

    private int _successSteps;
    
    private IMinigameService _minigameService;
    private IPetService _petService;
    private IMoodService _moodService;
    
    [Inject]
    private void ResolveDependencies(IMinigameService minigameService, IPetService petService, IMoodService moodService)
    {
        _petService = petService;
        _minigameService = minigameService;
        _moodService = moodService;
    }


    private void Start()
    {
        _minigameService.OnMinigameRequested += OnMinigameRequested;
        _minigameService.IngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(object sender, IngredientType ingredient)
    {
        _successSteps++;
        if (_successSteps >= recipeSteps) FinishMinigame();
    }

    private void FinishMinigame()
    {
        _successSteps = 0;
        _minigameService.SetInMinigame(false);
        _minigameService.OnGameOver(MinigameType.Cauldron);
        _moodService.AddMoodModifier(MoodTypeEnum.Hunger, new MoodModifier(30, false));
        _petService.ShowPet();
    }

    private void OnMinigameRequested(object sender, MinigameType e)
    {
        if(e != MinigameType.Cauldron)
            return;
        ActivateMinigame();
    }

    private void ActivateMinigame()
    {
        _minigameService.SetInMinigame(true);
        _petService.HidePet();
        var recipe = GetRandomRecipe((int)recipeSteps);
        _minigameService.CurrentRecipe = recipe;
        Debug.Log("Recipe: " + string.Join(", ", recipe));
    }

    private IngredientType[] GetRandomRecipe(int count)
    {
        IngredientType[] valuesArray = (IngredientType[])Enum.GetValues(typeof(IngredientType));
        Random random = new Random();
        valuesArray = valuesArray.OrderBy(x => random.Next()).ToArray();
        return valuesArray.Take(count).ToArray();
    }
}

public enum IngredientType
{
    Yellow,
    Green,
    Purple,
    Red,
    Blue,
    Orange
}
