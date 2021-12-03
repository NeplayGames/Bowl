using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bowling.Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] float distanceFromBall = -3f;
        [SerializeField] float height = 1f;
        [SerializeField] Transform bowlingBall;

        Vector3 offset;
        // Start is called before the first frame update
        void Start()
        {
            offset = new Vector3( bowlingBall.position.x,height,distanceFromBall);
            transform.position =new Vector3(0,0, bowlingBall.position.z)  + offset;
           
        }

        // Update is called once per frame
        
    }
}
