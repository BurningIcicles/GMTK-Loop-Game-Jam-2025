using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private string _objectId;
    private void Awake()
    {
        _objectId = this.gameObject.name;
    }
    void Start()
    {
        DontDestroyOnLoad[] dontDestroyObjects = FindObjectsOfType<DontDestroyOnLoad>();
        for (int i = 0; i < dontDestroyObjects.Length; i++)
        {
            DontDestroyOnLoad dontDestroyObject = dontDestroyObjects[i];
            if (dontDestroyObject == this)
                continue;

            if (dontDestroyObject.GetObjectId() == this.GetObjectId())
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(this);
    }

    private string GetObjectId()
    {
        return _objectId;
    }
}
