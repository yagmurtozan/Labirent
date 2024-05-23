using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
   [SerializeField] Transform target;
   Vector3 distance;

   private void Start()
   {
      distance = transform.position - target.position;
   }

   private void Update()
   {
      transform.position = target.position + distance;
   }
}
