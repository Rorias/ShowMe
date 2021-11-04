using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class DoorBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SpriteRenderer sr = animator.gameObject.GetComponent<SpriteRenderer>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        animator.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
