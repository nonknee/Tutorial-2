using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 startpos;
    public Vector2 endpos;

    public float speed = 1.0F;

    private float startTime;

    private float journeyLength;

    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector2.Distance(startpos, endpos);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startpos, endpos, Mathf.PingPong(fracJourney, 1));
    }
}