using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util  {

    public static float Pick (float x, float y){
        if (Random.value > 0.5f) {
            return x;
        } else {
            return y;
        }
    }

}
