using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AGD
{
    public class PlayerStats : MonoBehaviour
    {
        public Slider slider;
        public TMPro.TextMeshProUGUI score;
        public Gradient gradient;

        void Start(){
            score.text = "09";
        }

        public void SetMaxHealth(int health){
            slider.maxValue = health;
            slider.value = health;
        }

        public void setHealth(int health){
            slider.value = health;
        }
        
        public void setScore(int points){
            score.text = points.ToString().PadLeft(2, '0');
        }
    }
}
