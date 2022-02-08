using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{
    public GameObject deadPrefab;
    public GameObject cameraFollowPoint;

    GameManager gameManager;
    bool died;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (!died)
        {
            died = true;
            cameraFollowPoint.transform.parent = null;
            gameManager.WaitThenMoveToCameraToSpawnAndRespawn();
            Instantiate(deadPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
