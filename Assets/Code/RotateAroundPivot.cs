﻿using UnityEngine;

namespace Code
{
    public class RotateAroundPivot : MonoBehaviour
    {
        public float RotationSpeed;
        public Transform Pivot;

        private void Update()
        {
            transform.RotateAround(Pivot.position, Pivot.up, RotationSpeed * Time.deltaTime);
            transform.LookAt(Pivot);
        }
    }
}