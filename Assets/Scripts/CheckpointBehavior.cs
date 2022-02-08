using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehavior : MonoBehaviour
{
    public AudioSource checkPointSfx;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            spawnPoint.transform.position = transform.position;
            gameManager.NewCheckpoint();
            checkPointSfx.Play();
            Destroy(this.gameObject);
        }
    }
}
