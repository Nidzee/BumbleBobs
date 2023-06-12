using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class EachSecond : MonoBehaviour {
    
    public static EachSecond Instance {
        get {
            if (_instance == null) {
                GameObject manager = new GameObject("[EachSecond]");
                _instance = manager.AddComponent<EachSecond>();
                DontDestroyOnLoad(manager);
            }
            
            return _instance;
        }
    }
    static EachSecond _instance;


    public delegate void CallbackVoid();
    public class SignalVoid : UnityEvent {}



    public SignalVoid signalEachSecond = new SignalVoid();
    public SignalVoid signalEachFrame = new SignalVoid();   

    public SignalVoid signalOnPause = new SignalVoid();
    public SignalVoid signalOnResume = new SignalVoid();
    bool everyFrameIsRunning;




    
    void Start() 
    {
        LaunchEachSecondTrugger();    
        StartEachSecondCoroutine(OnEveryFrame());
    }



    // Trigger each second event
    void LaunchEachSecondTrugger()
    {
        InvokeRepeating("OnEachSecond", 1, 1);
    }
 
    void OnEachSecond() 
    {
        signalEachSecond.Invoke();
    }



    // Trigger each frame event
    private IEnumerator OnEveryFrame() 
    {
        // Prevent from spam
        if (everyFrameIsRunning) {
            yield break;
        }


        // Trigger each frame event
        everyFrameIsRunning = true;
        while(everyFrameIsRunning)
        {
            yield return null;
            signalEachFrame.Invoke();
        }
    }
    




    // App pause-unpause trigger
    void OnApplicationPause(bool paused) 
    {
        if(paused) 
        {
            signalOnPause.Invoke();
        } 
        else 
        {
            signalOnResume.Invoke();
        }
    }   
    



    // Delay custom actions logic
    public static void WaitSeconds(float afterSeconds, CallbackVoid callback) 
    {
        StartEachSecondCoroutine(WaitSecondsCoroutine(afterSeconds, callback));
    }
    
    static IEnumerator WaitSecondsCoroutine(float afterSeconds, CallbackVoid callback) 
    {
        yield return new WaitForSeconds(afterSeconds);
        callback?.Invoke();
    }




    // Helper fucntions
    public static void StartEachSecondCoroutine(IEnumerator routine) 
    {
        Instance.StartCoroutine(routine);
    }
}