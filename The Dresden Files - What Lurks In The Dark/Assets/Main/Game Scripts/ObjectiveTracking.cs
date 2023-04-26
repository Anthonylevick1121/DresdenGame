using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ObjectiveTracking : MonoBehaviour
{
    public static ObjectiveTracking instance;
    
    // if you're wondering what this is, this basically makes this sure there is only one loaded instance of this script
    // at any given time. It also makes it so that this script doesn't unload on scene change.
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    /*private struct Objective
    {
        public string name;
        public bool required;
    }*/

    // event called when a task is completed; event provides bool to denote if this was a required task
    public event Action<bool> OnTaskComplete;
    
    private PlayerCore player;
    private bool initialized = false; // have we set data for this level yet? 

    //private int levelIdx; // what objective set should we be using? // ignore, we're dyn alloc'ing our tasks
    private int requiredTasksDone; // how many have we completed?
    private int optionalTasksDone; // how many have we completed?
    
    private readonly List<(bool, string)> requiredTasks = new ();
    private readonly List<bool> optionalTasks = new ();
    //private List<Objective> optionalTasks = new ();
    // private int optionalTaskCount;
    
    //private static readonly Objective finalObjective = new Objective { required = true, name = "Go to sleep."};
    private static readonly string finalTask = "Go to sleep.";
    private UnityAction<Scene, Scene> sceneChangeAction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(requiredTasksDone < requiredTasks.Count)
                CompleteTask(requiredTasksDone);
        }
    }
    
    private void Start()
    {
        // we're gonna do most things on scene change
        //sceneChangeAction = (oldScene, newScene) => OnSceneChange(newScene);
        sceneChangeAction = (oldScene, newScene) => InitializeLevel();
        
        SceneManager.activeSceneChanged += sceneChangeAction;
        //OnSceneChange(SceneManager.GetActiveScene());
        InitializeLevel();
    }
    private void OnDestroy() => SceneManager.activeSceneChanged -= sceneChangeAction;
    
    private void InitializeLevel()
    {
        if (initialized) return;
        
        player = FindAnyObjectByType<PlayerCore>();
        initialized = player ? true : false;
    }
    
    private static string StrikeIf(bool strike, string s) => strike ? "<s>" + s + "</s>" : s;
    
    // this is just to make it more clear that other classes are not supposed to refresh the task list manually
    // this is ONLY for player ui loading.
    public void OnPlayerUILoad() => RefreshTaskListUI();
    
    // this should only be called on a valid level
    private void RefreshTaskListUI()
    {
        InitializeLevel();
        if (!player  || !player.ui) return;
        
        // go through the task list
        string tasks = "To do list:\n";
        foreach ((bool done, string task) in requiredTasks)
        {
            string line = "- " + task;
            tasks += StrikeIf(done, line) + "\n";
        }
        bool allDone = requiredTasksDone >= requiredTasks.Count;
        
        if(allDone)
            // add final objective
            tasks += "- " + finalTask + "\n";
        
        if (optionalTasks.Count > 0 && (allDone || optionalTasksDone > 0))
        {
            bool optionalsDone = optionalTasksDone >= optionalTasks.Count;
            tasks += "---\n<i>- " + StrikeIf(optionalsDone, "Optional: Explore more around the manor.");
        }
        
        player.ui.taskList.text = tasks;
    }
    
    // returns the ID of that task for it to give back later upon completion.
    public int AddTask(string name)
    {
        int size = requiredTasks.Count;
        requiredTasks.Add((false, name));
        RefreshTaskListUI();
        return size;
    }
    
    public void CompleteTask(int id)
    {
        (bool done, string name) = requiredTasks[id];
        if (done) return;
        requiredTasks[id] = (true, name);
        requiredTasksDone++;
        RefreshTaskListUI();
        OnTaskComplete?.Invoke(true);
    }
    
    public int AddOptional()
    {
        int size = optionalTasks.Count;
        optionalTasks.Add(false);
        return size;
    }

    public void CompleteOptional(int taskId)
    {
        if (optionalTasks[taskId]) return;
        optionalTasks[taskId] = true;
        optionalTasksDone++;
        OnTaskComplete?.Invoke(false);
    }

    /*private void OnSceneChange(Scene scene)
    {
        if (scene.name == "MainMenu")
        {
            //initialized = false;
            //levelIdx = -1;
            return;
        }
        
        // this might be null; will be null for any non-level level
        player = FindAnyObjectByType<PlayerCore>();
        if (!player) return;
        
        //levelIdx++; // from a non-game level, this will increment to zero.
    }*/
    
    // have we completed all but the final task for the day?
    public bool CanSleep() => requiredTasksDone >= requiredTasks.Count;
    
    // we need to know when the level will change before it happens anyway... so just change it here.
    public void AdvanceLevel()
    {
        ScreenFade.instance.FadeScreen(CanvasLayer.LevelTransition, () =>
        {
            requiredTasks.Clear();
            optionalTasks.Clear();
            requiredTasksDone = 0;
            optionalTasksDone = 0;
            initialized = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
