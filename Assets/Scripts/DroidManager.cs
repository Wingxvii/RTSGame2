using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroidType {
    Base,
}

public class DroidManager : MonoBehaviour
{
    #region SingletonCode
    private static DroidManager _instance;
    public static DroidManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //single pattern ends here
    #endregion  

    public List<Droid> Droidpool;
    public List<Droid> ActiveDroidPool;

    public int activeDroidCount = 0;

    //droid types
    public GameObject BaseDroid;

    // Start is called before the first frame update
    void Start()
    {
        //CALL THIS DURING LOADING SCREEN
        InitPool();
    }

    //init a pool of droids to use
    void InitPool()
    {
        for (int counter = 0; counter < Constants.MAX_SUPPLY; counter++)
        {
            Droidpool.Add(GameObject.Instantiate(BaseDroid, Vector3.zero, Quaternion.identity).GetComponent<Droid>());
        }

        foreach (Droid droid in Droidpool)
        {
            droid.gameObject.SetActive(false);
        }
    }


    //requests a drone to build, returns time to build
    public float RequestQueue(DroidType type)
    {
        if (activeDroidCount < Constants.MAX_SUPPLY)
        {

            switch (type)
            {
                case DroidType.Base:
                    return 5f;
                default:
                    Debug.Log("ERROR: DROID TYPE INVALID");
                    return -1f;
            }
        }
        Debug.Log("MAX SUPPLY REACHED");
        return -1f;
    }

    //called when drone is requested to be built
    public void QueueFinished(Barracks home, DroidType type)
    {
        switch (type)
        {
            case DroidType.Base:
                SpawnDroid(type, new Vector3(home.gameObject.GetComponent<Transform>().position.x + 8, 2, home.gameObject.GetComponent<Transform>().position.z));
                break;
            default:
                Debug.Log("ERROR: DROID TYPE INVALID");
                break;
        }
    }

    public void SpawnDroid(DroidType type, Vector3 pos) {
        Droidpool[Droidpool.Count - 1].gameObject.SetActive(true);
        Droidpool[Droidpool.Count - 1].transform.position = pos;

        ActiveDroidPool.Add(Droidpool[Droidpool.Count - 1]);
        Droidpool.RemoveAt(Droidpool.Count-1);
        activeDroidCount++;
    }

    public void KillDroid(Droid droid) {
        activeDroidCount--;
        
        Droidpool.Add(droid);
        ActiveDroidPool.Remove(droid);
    }
}

