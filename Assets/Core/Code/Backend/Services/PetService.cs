using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetService : IPetService
{
    public event EventHandler<bool> OnSetDirty;
    public event EventHandler OnPetHidden;
    public event EventHandler OnPetShown;
    public void SetDirty(bool isDirty) => OnSetDirty?.Invoke(this, isDirty);
    public void HidePet() => OnPetHidden?.Invoke(this, EventArgs.Empty);
    public void ShowPet() => OnPetShown?.Invoke(this, EventArgs.Empty);
}
