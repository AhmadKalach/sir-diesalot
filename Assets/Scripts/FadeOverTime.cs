using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOverTime : MonoBehaviour
{
    public float fadeStartTime;
    public float fadeDuration;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(fadeStartTime);
        sequence.Append(sprite.DOFade(0, fadeDuration));
        sequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
