using System;
using UnityEngine;

[CreateAssetMenu]
public class VoiceLineScript : ScriptableObject
{
    [Serializable]
    public struct VoiceLine
    {
        public AudioClip clip;
        public float volume;
        public string subtitle;
    }
    
    [SerializeField] public float volume = 1;
    [SerializeField] private AudioClip[] voiceLines;
    [SerializeField] private string[] voiceLineScripts;
    
    public VoiceLine GetLine(VoiceLineId id) => new()
    {
        clip = voiceLines[(int) id],
        volume = volume,//voiceLines[(int) id]?.name.Contains("Wizard") ?? true ? wizardVolume : demonVolume,
        subtitle = voiceLineScripts[(int) id]
    };
}

public enum VoiceLineId
{
    None, 
    
    Voicemail1, Voicemail2, Voicemail3, DresdenInterrupt,
    
    TutTask1, TutTask2, TutTaskFinal,
    
    ObjNecklace, ObjUrn, ObjLoveNotes, ObjPictures, ObjBuisCard, ObjDiagnosis,
    ObjPills1, ObjPills2, ObjPills3
}