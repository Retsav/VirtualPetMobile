using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestService : ITestService
{
    public void HelloWorld()
    {
        Debug.Log("Hello world!");
    }
}
