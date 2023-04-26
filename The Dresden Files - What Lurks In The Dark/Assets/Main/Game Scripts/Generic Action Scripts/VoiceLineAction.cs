using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineAction : MonoBehaviour
{
    public VoiceLineId voiceLine;
    
    public void PlayVoiceLine()
    {
        VoicePlayer.instance.PlayVoiceLine(voiceLine);
    }
}
