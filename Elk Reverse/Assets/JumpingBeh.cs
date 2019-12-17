using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBeh : StateMachineBehaviour {

    private static float lastSpeed = 0.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //UnityEngine.Debug.Log(animator.GetCurrentAnimatorClipInfo(layerIndex)[layerIndex].clip.name + "   " + lastSpeed);
        //Debug.Log(layerIndex);
        if (!animator.GetCurrentAnimatorClipInfo(layerIndex)[layerIndex].clip.name.Contains("Start"))
            animator.SetBool("TransitionJump", false);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float speed = animator.GetFloat("SpeedY");
        if (speed > lastSpeed)
        {
            lastSpeed = speed;
        }
        else // We are falling
        {
            animator.SetBool("TransitionJump", true);
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
