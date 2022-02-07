using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class Wait : ActionNode {
        public float duration = 1;
        float startTime;

        protected override void OnStart() {
            startTime = Time.time;
            //Debug.Log("Ejecutando espera del NPC!");
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            if (Time.time - startTime > duration) {
                //Debug.Log("Finalizada espera del NPC!");
                return State.Success;
            }
            return State.Running;
        }
    }
}
