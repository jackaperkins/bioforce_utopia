using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuitHandler : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        if (!Application.isEditor)
        {
            Debug.Log("We got quit!");
            UtopiaUtilities.WriteRelayFile();
        }
    }
}
