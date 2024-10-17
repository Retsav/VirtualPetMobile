using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetService : IPetService
{
    public event EventHandler<bool> OnSetDirty;
    public void SetDirty(bool isDirty) => OnSetDirty?.Invoke(this, isDirty);
}
