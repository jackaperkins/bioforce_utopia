﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    public static Camera current;

    void Awake() {

        current = GetComponent<Camera>();
    }

}
