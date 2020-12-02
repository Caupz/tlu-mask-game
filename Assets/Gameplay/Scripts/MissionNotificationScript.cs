using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNotificationScript : MonoBehaviour
{
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > currentTime)
        {
            Destroy(gameObject);
        }
    }
}
