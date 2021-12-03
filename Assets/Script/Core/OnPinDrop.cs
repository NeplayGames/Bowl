using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bowling.Core
{
    public class OnPinDrop : MonoBehaviour
    {
        public static event Action FallDown;
        bool hasTouchGround = false;

        private  void OnTriggerEnter(Collider other) {
            
       
            if(hasTouchGround) return;
            if (other.CompareTag("Ground"))
            {
                hasTouchGround = true;
                if (FallDown != null)
                    FallDown();

                Invoke(nameof(DestroyAfterTime),1f);
            }
        }

        private void DestroyAfterTime()
        {
           Destroy(transform.parent.parent.gameObject);
        }
    }
}


