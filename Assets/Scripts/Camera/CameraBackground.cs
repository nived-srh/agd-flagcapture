using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class CameraBackground : MonoBehaviour
    {
        public Color color1 = Color.red;
        public Color color2 = Color.blue;
        public float duration = 3.0F;

        public Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;

        }

        // Update is called once per frame
        void Update()
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            cam.backgroundColor = Color.Lerp(color1, color2, t);
        }
    }
}
