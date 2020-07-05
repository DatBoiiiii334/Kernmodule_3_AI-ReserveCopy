using Pada1.BBCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckForRage1")]
    [Help("Checks if the gameObject is enraged")]
    public class CheckForRage1 : GOCondition
    {
        [InParam("Mono")]
        [Help("To acces the monobehavior of the gameObject")]
        public MonoHP _monoInstance;

        [InParam("rageTimer")]
        [Help("will stop raging when it hits zero")]
        public float rageTimer;

        public override bool Check()
        {
            if(rageTimer > 0) {
                rageTimer -= 1 * Time.deltaTime;
                if (_monoInstance.IsRaging == true) {
                    return true;
                }
                
            }
            return false;
        }
    }
}
