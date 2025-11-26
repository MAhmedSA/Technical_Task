using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;
    public static CoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CoroutineManager");
                _instance = go.AddComponent<CoroutineManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private Dictionary<IEnumerator, CoroutineData> coroutines = new Dictionary<IEnumerator, CoroutineData>();

    private class CoroutineData
    {
        public Coroutine coroutine;
        public bool isPaused;
        public bool isRunning;
    }

    // Start a coroutine
    public void StartRoutine(IEnumerator routine)
    {
        if (coroutines.ContainsKey(routine))
        {
            Debug.LogWarning("Coroutine already running!");
            return;
        }

        CoroutineData data = new CoroutineData();
        data.coroutine = StartCoroutine(RunRoutine(routine, data));
        data.isPaused = false;
        data.isRunning = true;

        coroutines.Add(routine, data);
    }

    // Internal wrapper to handle pause
    private IEnumerator RunRoutine(IEnumerator routine, CoroutineData data)
    {
        while (true)
        {
            while (data.isPaused)
                yield return null;

            if (!routine.MoveNext())
                break;

            yield return routine.Current;
        }

        data.isRunning = false;
        coroutines.Remove(routine);
    }

    // Pause coroutine
    public void Pause(IEnumerator routine)
    {
        if (coroutines.ContainsKey(routine))
            coroutines[routine].isPaused = true;
    }

    // Resume coroutine
    public void Resume(IEnumerator routine)
    {
        if (coroutines.ContainsKey(routine))
            coroutines[routine].isPaused = false;
    }

    // Stop coroutine
    public void Stop(IEnumerator routine)
    {
        if (coroutines.ContainsKey(routine))
        {
            StopCoroutine(coroutines[routine].coroutine);
            coroutines.Remove(routine);
        }
    }

    // Check if running
    public bool IsRunning(IEnumerator routine)
    {
        return coroutines.ContainsKey(routine) && coroutines[routine].isRunning;
    }

    // Check if paused
    public bool IsPaused(IEnumerator routine)
    {
        return coroutines.ContainsKey(routine) && coroutines[routine].isPaused;
    }
}

