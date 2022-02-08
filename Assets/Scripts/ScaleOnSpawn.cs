using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleOnSpawn : MonoBehaviour
{
    public float scaleTime;

    Vector2 targetScale; 

    // Start is called before the first frame update
    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector2.zero;
        transform.DOScale(targetScale, scaleTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
