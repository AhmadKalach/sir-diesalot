using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterBehavior : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float startTime;
    public float timeBetweenArrows;
    public float arrowSpeed;

    float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = Time.time + startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShotTime + timeBetweenArrows)
        {
            lastShotTime = Time.time;
            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * arrowSpeed;
        }
    }
}
