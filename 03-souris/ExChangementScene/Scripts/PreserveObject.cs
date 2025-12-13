using UnityEngine;

public class PreserveObject : MonoBehaviour
{
    public static bool didPreserveObject = false;
    public static PreserveObject Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (!didPreserveObject)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    didPreserveObject = true;
        //}

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
