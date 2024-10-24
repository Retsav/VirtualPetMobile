using System;

public interface IPetService
{
    public event EventHandler<bool> OnSetDirty;
    public void SetDirty(bool isDirty);
}
