using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public NewScriptableObjectScript exampleScriptableObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(exampleScriptableObject.exampleID);
        Debug.Log(exampleScriptableObject.exampleName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
