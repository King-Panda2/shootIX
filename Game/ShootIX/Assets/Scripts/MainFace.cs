using UnityEngine;

public class MainFace : MonoBehaviour
{
    // TO DO: randomization functionality
    
    public static MainFace Instance;

    private Face face;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        face = gameObject.GetComponent<Face>();
    }
}
