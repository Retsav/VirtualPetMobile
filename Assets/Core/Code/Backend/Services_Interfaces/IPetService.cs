using System;

public interface IPetService
{
    public event EventHandler<bool> OnSetDirty;
    public event EventHandler OnPetHidden;
    public event EventHandler OnPetShown;
    public void SetDirty(bool isDirty);
    public void HidePet();
    public void ShowPet();
}
