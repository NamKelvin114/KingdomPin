using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Stick
{
    public class StickTouch : MonoBehaviour
    {
        public bool IsCutting { get; set; } = false;
        private Rigidbody rb;
        private Camera cam;
        public GameObject Trail;
        private Vector2 previousposition;
        private float minveloc = 0.001f;
        BoxCollider boxCollider;
        public TrailRenderer TrailRenderer;

        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            cam = Camera.main;
            rb = GetComponent<Rigidbody>();
            boxCollider.enabled = false;
        }

        void OnEnable()
        {
            Lean.Touch.LeanTouch.OnFingerDown += StartCutting;
            Lean.Touch.LeanTouch.OnFingerUp += StopCutting;
            Lean.Touch.LeanTouch.OnFingerUpdate += UpdateCut;
        }

        void OnDisable()
        {
            Lean.Touch.LeanTouch.OnFingerDown -= StartCutting;
            Lean.Touch.LeanTouch.OnFingerUp -= StopCutting;
            Lean.Touch.LeanTouch.OnFingerUpdate -= UpdateCut;
        }

        // Update is called once per frame
        // void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Debug.Log("dsa");
        //         StartCutting();
        //     }
        //     else if (Input.GetMouseButtonUp(0))
        //     {
        //         StopCutting();
        //     }
        //
        //     if (IsCutting)
        //     {
        //         UpdateCut();
        //     }
        // }

        void UpdateCut(Lean.Touch.LeanFinger finger)
        {
            Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var veloc = (newPosition - previousposition).magnitude * Time.deltaTime;
            if (veloc > minveloc)
            {
                Trail.gameObject.SetActive(true);
            }

            rb.position = newPosition;
        }

        void StartCutting(Lean.Touch.LeanFinger finger)
        {
            Debug.Log("down");
            Trail.gameObject.SetActive(false);
            TrailRenderer.Clear();
            boxCollider.enabled = true;
            IsCutting = true;
            previousposition = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        void StopCutting(Lean.Touch.LeanFinger finger)
        {
            boxCollider.enabled = false;
            Trail.gameObject.SetActive(false);
            IsCutting = false;
        }
    }
}