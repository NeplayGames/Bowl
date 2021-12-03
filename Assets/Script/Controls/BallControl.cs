using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bowling.Core;
using UnityEngine.UI;

namespace Bowling.Control
{
    public class BallControl : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private float forceMultiplier = 50;
       // [SerializeField] private float minimumDistance = .2f;
        [SerializeField] private float maxmiumTime = 1f;
       // [SerializeField] private Transform leftLimit;
       // [SerializeField] private Transform rightLimit;

       [SerializeField] private int totalPlay;
        [SerializeField] GameObject instruction;
       [SerializeField] PinScoreCounter pinScoreCounter;
       private int currentPlay;
        bool canDrag = false;

        bool addFroce = false;

        bool isLaunched = false;
        float time;
         [SerializeField] Text totolServe;

        Vector2 initalTouchPosition;
      //  float finalTouchPosition;

        Vector3 InitialBallPosition;
        [SerializeField] AudioSource audioSource;
        private void Start()
        {
            InitialBallPosition = transform.position;
            rb = GetComponent<Rigidbody>();
                 totolServe.text = "Total Serve : " + currentPlay+ "/"+ totalPlay;

        }
        private void Update()
        {
            InputClass();
        }


        ///<summary>
        ///Manage the input through mouse or touch
        ///</summary>
        private void InputClass()
        {
            if (isLaunched) return;
            if (Input.GetMouseButtonDown(0))
            {
                instruction.SetActive(false);
                CheckIfMouseTouchBall();

            }
            // if (Input.GetMouseButton(0) && canDrag)
            // {
            //     ChangeBallPosition();
            // }
            if (Input.GetMouseButtonUp(0) && canDrag)
            {

                DetectSwipe();
            }
        }

        ///<summary>
        ///Determine if the mouse or touch is touching the ball
        ///</summary>
        private void CheckIfMouseTouchBall()
        {
            time = Time.time;
            initalTouchPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if (Physics.Raycast(ray))
            {
              
                    canDrag = true;
                }
            

        }


        ///<summary>
        ///Change the ball position according to the touch of the ball
        ///</summary>
        // private void ChangeBallPosition()
        // {
        //     //Determine the distance between the camera and the ball
        //     float dist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        //     Vector3 inputPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        //     ///Identify the position in world point
        //     inputPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        //     Vector3 pos = transform.position;
        //     //Get position x values
        //     pos.x = inputPosition.x;
        //     //Apply it to the ball
        //     ClampPosition(pos);
        // }

        ///<summary>
        ///Clamp the given position between given left and right position
        ///</summary>
        // private void ClampPosition(Vector3 pos)
        // {
        //     // transform.position =
        //     // new Vector3(Mathf.Clamp(pos.x, leftLimit.position.x, rightLimit.position.x),
        //     // pos.y,
        //     // pos.z);
        // }

        int curve = 0;
        bool isShooting = false;
        ///<summary>
        ///Determine if there is swipe by the player through mouse or touch
        ///</sumamry>
        private void DetectSwipe()
        {
                canDrag = false;

            float force = Mathf.Abs(initalTouchPosition.y - Input.mousePosition.y);
            if (Time.time - time < maxmiumTime && addFroce)
            {
                
                rb.AddForce(new Vector3(0, 0,Mathf.Clamp(force * forceMultiplier,3000,3500)));
                isShooting = true;
                audioSource.Play();
                isLaunched = true;
            }
            curve = (int)( Input.mousePosition.x-initalTouchPosition.x);
            addFroce = true;
        }

        private bool CheckIfGameFinished()
        {
            return totalPlay == currentPlay;
            
        }

        void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("EndPoint"))
            {
                currentPlay++;
                 totolServe.text = "Total Serve : " + currentPlay+ "/"+ totalPlay;
                Invoke(nameof(Reset), 2f);
                return;
            }
        }
        void FixedUpdate()
        {
            if(!isShooting) return;
            Vector3 forceToApply = rb.angularVelocity* curve/30 + (rb.velocity );
             rb.AddForce(new Vector3(forceToApply.x,0,forceToApply.z));
        }
        private void Reset()
        {
            isShooting = false;
            if(CheckIfGameFinished()){
                pinScoreCounter.GameOver();
                Destroy(this.gameObject);
                return;
            }
            instruction.SetActive(true);
            transform.position = InitialBallPosition;
            isLaunched = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

