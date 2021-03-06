using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {

        public Vector3 moveToPosition;

        public float firstThreshold = 0f;
        public float secondThreshold = 0f;
        public float thirdThreshold = 0f;

        public float firstTaskTime = 80f;
        public float secondTaskTime = 80f;
        public float thirdTaskTime = 80f;

        public float maxTime = 80f;
        public float probability = 0f;
        public int priorityTask = 0;

        public string[] Tasks;
        public Vector3 RandomPosition;
        public Vector2 min = new Vector2(-8.8f, -18.4f);
        public Vector2 max = new Vector2(8.8f, 14.7f);  

    }
}