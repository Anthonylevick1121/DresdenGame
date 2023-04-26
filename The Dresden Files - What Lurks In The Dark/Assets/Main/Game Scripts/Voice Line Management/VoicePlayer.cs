using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    
    private int cachedClip = -1;
    
    // track currently-playing audio from non-main players.
    private List<AudioSource> activeExternalPlayers = new ();
    private float nextPlayerCheck;

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
        if(pause) voicePlayer.Pause();
        else voicePlayer.UnPause();
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
