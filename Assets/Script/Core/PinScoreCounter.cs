namespace Bowling.Core{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class PinScoreCounter : MonoBehaviour {

        
        [Header("The total number of pin in the given level")]
        [SerializeField] int totalPin = 6;
        [SerializeField] AudioClip clip;
        int counter = 0;
        [SerializeField] GameObject pin10;
        [SerializeField] GameObject GameOverCanvas;
        [SerializeField] Text totolPinDrop;
        [SerializeField] Text totolScore;
        [SerializeField] Text finalPinsCount;
        [SerializeField] Text finalScoreCount;
        AudioSource audioSource;
        int pinInstantiated = 0;

        private void Start() 
        {
           //Register the event to the coin Drop
           OnPinDrop.FallDown += OnPinFall;
           audioSource = GetComponent<AudioSource>();
        }

        private void OnDestroy() 
        {
             OnPinDrop.FallDown -= OnPinFall;
        }

        ///<summary>
        ///If the pin falls add the pin to the counter
        ///</summary>
        private void OnPinFall()
        {
            counter++;
            totolPinDrop.text = "Total Pins:" + counter;
            CheckPinCount();
        }
        private void CheckPinCount()
        {      
            if(counter==totalPin * (pinInstantiated + 1))
            {
                audioSource.clip = clip;
                audioSource.Play();
                pinInstantiated++;
                totolScore.text = "Total Score : " + 100 * pinInstantiated;
                Invoke(nameof(InstantePin),1f);
            }
        }

        private void InstantePin()
        {
            Instantiate(pin10);
        }

        public void GameOver()
        {
            GameOverCanvas.SetActive(true);
            totolPinDrop.gameObject.SetActive(false);
            totolScore.gameObject.SetActive(false);
            finalPinsCount.text = "Total Pins:" + counter;
            finalScoreCount.text = "Total Score : " + 100 * pinInstantiated;
            HttpCookie.SetCookie("Score", (100 * pinInstantiated).ToString());
        }       
    }
} 