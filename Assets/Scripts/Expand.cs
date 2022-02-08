using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Expand : MonoBehaviour
{
    public float expandTime;
    public Vector2 expandScale;

    Vector2 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;

        transform.DOScale(expandScale * initialScale, expandTime);
    }
}
