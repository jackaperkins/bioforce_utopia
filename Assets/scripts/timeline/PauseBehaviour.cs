using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseBehaviour : PlayableBehaviour
{
    bool spawned;

    public override void PrepareFrame(Playable playable, FrameData data)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        // as soon as we get a single frame of our clip, check to see if we spawned the bubble
        if (!spawned)
        {
            spawned = true;
            Debug.Log("BEEP");
            //if (origin == null)
            //{
            //    Debug.Log("missing origin position for dialog '" + text + "'");
            //    return;
            //}

            //float lifetime = (float)(playable.GetDuration() - playable.GetTime());
            //bubbleInstance = DialogCanvas.AddBubble(text, origin, lifetime);
        }


    }

}