using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MgsCommonLib.Animation
{
    public static class AnimatorExtentions
    {
        public static bool HasTrigger(this Animator animator, string triggerName)
        {
            return animator
                .parameters
                .Any(p => p.type == AnimatorControllerParameterType.Trigger && p.name == triggerName);

        }
        public static IEnumerator SetTriggerAndWaitForTwoStateChanges(this Animator animator, string triggerName)
        {
            if (!animator.HasTrigger(triggerName))
            {
                Debug.LogError(
                    $"Animator in {animator.name} don't have triger \"{triggerName}\" !!!");
                yield break;
            }

            yield return new WaitForEndOfFrame();
            animator.SetTrigger(triggerName);
            yield return animator.WaitForChangeState();
            yield return animator.WaitForChangeState();

        }
        public static IEnumerator WaitForChangeState(this Animator animator)
        {
            var startNameHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

            while (startNameHash == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
                yield return null;
        }
    }
}
