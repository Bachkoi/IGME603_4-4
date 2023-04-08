using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AfterGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Jump(string name)
    {
        SceneManager.LoadScene(name);
        Debug.Log("jump to " + name);
    }

    public void Continue()
    {
        SceneManager.LoadScene("Sam Test");
    }
}
