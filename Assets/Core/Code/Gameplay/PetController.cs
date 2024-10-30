using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PetController : MonoBehaviour
{
    [SerializeField] private CleaningMinigame cleaningSystem;
    [SerializeField] private GameObject petGameObject;
    

    private IPetService _petService;

    [Inject]
    private void ResolveDependencies(IPetService petService)
    {
        _petService = petService;
    }

    private void Start()
    {
        _petService.OnSetDirty += PetService_OnSetDirty;
        _petService.OnPetHidden += OnPetHidden;
        _petService.OnPetShown += OnPetShown;
    }

    private void OnPetShown(object sender, EventArgs e) => ShowPet();
    private void OnPetHidden(object sender, EventArgs e) => HidePet();

    private void HidePet()
    {
        foreach (Transform child in petGameObject.transform) child.gameObject.SetActive(false);   
    }
    
    private void ShowPet()
    {
        foreach (Transform child in petGameObject.transform) child.gameObject.SetActive(true);   
    }

    private void PetService_OnSetDirty(object sender, bool isDirty)
    {
        if (isDirty)
            cleaningSystem.ShowDirty();
        else
            cleaningSystem.HideDirty();
    }

    private void OnDestroy()
    {
        _petService.OnSetDirty -= PetService_OnSetDirty;
    }
}
