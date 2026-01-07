using UnityEngine;

public class Enemy : MonoBehaviour
{   public static Enemy Instance { get; private set; }
    void Start()
    {
        
    }
    private void Awake() 
    {
   
       
        Instance = this;
    }

    void Update()
    {
        
    }
}
