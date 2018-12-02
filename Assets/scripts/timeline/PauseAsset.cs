
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PauseAsset : PlayableAsset {

    [HideInInspector,SerializeField]
    public string id = "";

    public void Awake () {

    }


    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner) {
        PauseBehaviour b = new PauseBehaviour();

        ScriptPlayable<PauseBehaviour> p = ScriptPlayable<PauseBehaviour>.Create(graph, b);
        return p;
    }
        
    public void OnDestroy () {
        Debug.Log ("Im dying, and my ID was " + id);

    }
}