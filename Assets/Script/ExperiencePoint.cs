using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    public float value;
    public Player playerRef;

    // maximum speed that the exp point can reach when lerping towards player
    public float maxSpeed = 50f;



    // maximum distance that this can go towards the player.
    public float MaxDist => playerRef.maxSuckDist;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(playerRef != null);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Mathf.Abs( Vector3.Distance(this.transform.position, playerRef.transform.position));
        float adjustedSpeed = Mathf.Lerp(maxSpeed, 0, distanceToPlayer / MaxDist);
        float step = adjustedSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(
            this.transform.position,
            playerRef.transform.position,
            step);
    }

    public void OnTriggerEnter(Collider other)
    {
        Player playerCollecting = other.GetComponent<Player>();
        if ( playerCollecting == null )
        {
            return;
        }
        playerCollecting.GainXP(this.value);
        //playsound
        Destroy(this.gameObject);
    }
}
