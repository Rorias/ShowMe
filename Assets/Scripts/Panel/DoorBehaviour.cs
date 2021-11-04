using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class DoorBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }
}
