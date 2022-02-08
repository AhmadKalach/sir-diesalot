using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeBehavior : MonoBehaviour
{
    public float waitUntilSpikes;
    public float spikesTime;
    public Vector3 hitboxSize;
    public LayerMask playerLayer;

    bool attacking;
    bool attackStarted;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, hitboxSize, 0, playerLayer);

        if (hit != null)
        {
            if (attacking)
            {
                hit.gameObject.GetComponent<PlayerState>().Die();
            }
            else
            {
                if (!attackStarted)
                {
                    Attack();
                    attackStarted = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, hitboxSize);
    }

    void Attack()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            animator.SetTrigger("Ready");
        });
        sequence.AppendInterval(waitUntilSpikes);
        sequence.AppendCallback(() =>
        {
            animator.SetTrigger("Attack");
            attacking = true;
        });
        sequence.AppendInterval(spikesTime);
        sequence.AppendCallback(() =>
        {
            animator.SetTrigger("Idle");
            attacking = false;
            attackStarted = false;
        });

        sequence.Play();
    }
}
