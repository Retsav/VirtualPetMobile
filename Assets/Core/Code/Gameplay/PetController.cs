using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PetController : MonoBehaviour
{
    [SerializeField] private CleaningSystem cleaningSystem;

    private IPetService _petService;

    [Inject]
    private void ResolveDependencies(IPetService petService)
    {
        _petService = petService;
        _petService.OnSetDirty += PetService_OnSetDirty;
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
