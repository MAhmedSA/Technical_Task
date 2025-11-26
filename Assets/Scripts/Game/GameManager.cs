using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int _currentWave =1;
    public int CurrentWave { get { return _currentWave; } }
    int totalEnemies=0;
    bool canSpawn = true;

    bool waveCompleted = false;
    public  bool CanSpwan { get { return canSpawn; } set { canSpawn = value; } }

    [Header("Time Variables")]
    [Tooltip("Time Between Spawns")]
    float timeBetweenSpawns = 3f;
    float waveTimeRespwan = 5f;
    IEnumerator waveCoroutine;
   

    [SerializeField] ObjectPool poolingSystem;

    [SerializeField] Stack<PooledObject> freeEnemies; // stack to hold free enemies

    public static GameManager instance;

    private void Awake()
    {
       
        if (poolingSystem == null) { 
            poolingSystem = FindObjectOfType<ObjectPool>();
        
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        freeEnemies= new Stack<PooledObject>();
        if (canSpawn)
        {
            waveCoroutine = GenerateWave();
            CoroutineManager.Instance.StartRoutine(waveCoroutine);
           
        }
    }

    public IEnumerator NextWave() {
        _currentWave++;
        UIManager.instance.UpdateTextWave();
        switch (_currentWave)
        {
            case 2:
                yield return new WaitForSeconds(waveTimeRespwan);
                poolingSystem.InitPoolSize +=20;
                if (canSpawn) {
                    waveCoroutine = null;
                    waveCoroutine = GenerateWave();
                    CoroutineManager.Instance.StartRoutine(waveCoroutine);
                 

                }
                break;
            case 3:
                yield return new WaitForSeconds(waveTimeRespwan);
                poolingSystem.InitPoolSize += 20;
                if (canSpawn)
                {
                    waveCoroutine = null;
                    waveCoroutine = GenerateWave();
                    CoroutineManager.Instance.StartRoutine(waveCoroutine);

                }
                break;
            default:
                if (_currentWave >= 4) {
                    poolingSystem.InitPoolSize += 10;
                    if (canSpawn)
                    {
                        waveCoroutine = null;
                        waveCoroutine = GenerateWave();
                        CoroutineManager.Instance.StartRoutine(waveCoroutine);
                    }

                }
                break;
        }
    }

    IEnumerator GenerateWave()
    {
        
        for (int i = 0; i < poolingSystem.InitPoolSize; i++) { 
        
            yield return new WaitForSeconds(timeBetweenSpawns);
            freeEnemies.Push(poolingSystem.GetPooledObject());
            totalEnemies = freeEnemies.Count;
            UIManager.instance.UpdateTextEnemies(totalEnemies);
        }
        waveCompleted = true;
        AutoGenerateWave();

    }

    void AutoGenerateWave() {
     
        waveCompleted = false;
        CoroutineManager.Instance.StartRoutine(NextWave()); 
    }

    public void DestroyEnemies() { 
        Debug.Log(" All Enemies"+ freeEnemies.Count);
        for (int i = freeEnemies.Count-1; i >=0; i--)
        {

            PooledObject enemy = freeEnemies.Pop();
            poolingSystem.ReturnToPool(enemy);
            totalEnemies = 0;
            UIManager.instance.UpdateTextEnemies(totalEnemies);
        }

    }
    public void StopGenerateEnemies() {
        if (waveCoroutine == null) { 
             Debug.Log(" No Wave Coroutine to Stop ");
                return;
        }
        if (waveCoroutine != null) {
            Debug.Log("wave Coroutine exist");
            CoroutineManager.Instance.Pause(waveCoroutine);
        }
   
    }
    public void CheckCanGenerate() {
        if (canSpawn)
        {
            if (waveCoroutine != null && !waveCompleted)
            {
                CoroutineManager.Instance.Resume(waveCoroutine);
               
            }
            if (waveCoroutine==null&& waveCompleted)
            {
                waveCoroutine = GenerateWave();
                CoroutineManager.Instance.StartRoutine(waveCoroutine);
            }

        }
        else {
            StopGenerateEnemies();
            Debug.Log(" Spawning Stopped ");
        }
    }
}
