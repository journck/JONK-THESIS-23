using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("Dynamic")]
    public GameObject toFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow == null)
        {
            return;
        }
        else
        {
            Vector3 toFollowLocation = new Vector3(
                toFollow.transform.position.x,
                toFollow.transform.position.y,
                this.transform.position.z);
            this.transform.position = toFollowLocation;
        }
    }
}
