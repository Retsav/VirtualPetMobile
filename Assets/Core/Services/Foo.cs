using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Foo : IInitializable, IDisposable
{
    public void Initialize()
    {
        Debug.Log("Foo.Initialize");
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
