using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DependencyDebugger : MonoBehaviour
{
    private ITestService _testService;


    [Inject]
    private void ResolveDependencies(ITestService testService)
    {
        _testService = testService;
    }
    
    
    private void Start()
    {
        _testService.HelloWorld();
    }
    
}
