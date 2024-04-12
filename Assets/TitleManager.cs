using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    bool showingControls = false;
    public GameObject controlObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (!showingControls)
            {
                controlObj.SetActive(true);
                showingControls = true;
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}
