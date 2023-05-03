using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(AudioSource))]
// make sure to add this if you want the voice player to pause with the game! But sure, you don't *have* to.
//[RequireComponent(typeof(PausableAudio))]
public class VoicePlayer : MonoBehaviour
{
    private static VoicePlayer inst;
    public static VoicePlayer instance
    {
#if UNITY_EDITOR
        get
        {
            if (inst) return inst;
            // statically load into inst
            GameObject obj = (GameObject) PrefabUtility.InstantiatePrefab(
                AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Main/Game Scripts/Voice Line Management/Voice Player.prefab"));
            inst = obj.GetComponent<VoicePlayer>();
            DontDestroyOnLoad(obj);
            return inst;
        }
#else
        get => inst;
#endif
    }
    
    private void Awake()
    {
        if (inst != null)
        {
            if(inst != this) Destroy(gameObject);
            // finding another means we reloaded the main menu scene; don't play voice clips through that
            if (inst.voicePlayer)
            {
                inst.voicePlayer.Stop();
                inst.subtitle.text = "";
                inst.subtitleTimer = 0;
            }
            
            return;
        }
        
        inst = this;
        DontDestroyOnLoad(gameObject);
    }
    
    [SerializeField] public VoiceLineScript voiceScript;
    [SerializeField] private AudioSource voicePlayer;
    [SerializeField] private TextMeshProUGUI subtitle;
    private float subtitleTimer;
    private string captions;
    private bool paused;
    
    // any audio that pauses in the pause menu should subscribe to this event.
    // PausableAudio is a script you can attach to an AudioSource which handles this for you.
    public event Action<bool> OnAudioPause;
    
    private int cachedClip = -1;
    
    // Start is called before the first frame update
    private void Start()
    {
        subtitle.text = "";
        subtitle.canvas.sortingOrder = (int) CanvasLayer.Subtitles;
        if (cachedClip >= 0)
            PlayVoiceLine((VoiceLineId) cachedClip);
    }
    
    private void Update()
    {
        if (paused || subtitleTimer <= 0) return;
        if (captions != null)
        {
            subtitle.text = captions;
            captions = null;
        }
        
        subtitleTimer -= Time.deltaTime;
        if (subtitleTimer <= 0) subtitle.text = "";
    }
    
    /// just as a note: any other audio players played through the below methods should prolly be paused by a
    /// FindObjectsWithTag where every audio player is tagged.
    public void SetPaused(bool pause)
    {
        paused = pause;
        // pause all interested audio players
        OnAudioPause?.Invoke(pause);
    }

    private static float CalcDelay(float baseDelay, AudioClip clip, string captions)
    {
        return baseDelay + (clip ? clip.length : captions.Split(" ").Length / 3f) + 2;
    }

    public float PlayVoiceLine(VoiceLineId id) => PlayVoiceLine(id, 0.75f);
    public float PlayVoiceLine(VoiceLineId id, float delay)
    {
        if (!voicePlayer)
        {
            cachedClip = (int) id;
            VoiceLineScript.VoiceLine line = voiceScript.GetLine(id);
            return CalcDelay(delay, line.clip, line.subtitle);
        }
        else
            return PlayVoiceLine(id, delay, voicePlayer);
    }
    /// we may want to play captioned voice lines from external sources sometimes, so we can do it here.
    public float PlayVoiceLine(VoiceLineId id, float delay, AudioSource player)
    {
        print("play voice line "+Enum.GetName(typeof(VoiceLineId), id));
        player.Stop();
        VoiceLineScript.VoiceLine line = voiceScript.GetLine(id);
        player.clip = line.clip;
        if (player.clip)
        {
            player.volume = line.volume;
            player.PlayDelayed(delay); // delay to account for fade time, etc
        }
        
        captions = line.subtitle;
        subtitleTimer = CalcDelay(delay, player.clip, captions);
        return subtitleTimer;
    }
}
