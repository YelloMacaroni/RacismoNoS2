using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCENE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private static SCENE instance = null;
    public static SCENE Instance
    {
        get{return instance;}
    }
    void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance=this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
