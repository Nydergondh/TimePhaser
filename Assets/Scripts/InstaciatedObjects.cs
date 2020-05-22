using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaciatedObjects : MonoBehaviour
{
    public static InstaciatedObjects fatherReference;

    private void Awake() {
        if(fatherReference != null) {
            fatherReference = null;
        }
        fatherReference = this;
    }
}
