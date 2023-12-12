using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCodeStyle : MonoBehaviour {

    // Constants: UpperCase SnakeCase
    public const int CONSTANT_FIELD = 56;

    // Properties: PascalCase
    public static MyCodeStyle Instance { get; private set; }

    // Events: PascalCase
    // public event EventHandler OnSomethingHappened;

    // Fields: camelCase
    private float memberVariable;

    // Function Names: PascalCase
    private void Awake() {
        Instance = this;
        DoSomething(10f);
    }

    // Function Params: camelCase
    private void DoSomething(float time) {
        // Do something...
        memberVariable = time + Time.deltaTime;
        if (memberVariable > 0) {
            // Do something else...
        }
    }
}
