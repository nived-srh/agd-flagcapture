using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ColorChange : MonoBehaviour
    {
        public SpriteRenderer item;
        public Color color1 = Color.red;
        public Color color2 = Color.blue;
        public float duration = 3.0F;
        // Start is called before the first frame update
        void Start()
        {
            item = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            item.color = Color.Lerp(color1, color2, t);
        }
    }
}
