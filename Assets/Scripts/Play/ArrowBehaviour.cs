using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

namespace com.imie.geocaching
{
    public class ArrowBehaviour : MonoBehaviour
    {
        public GameObject Camera;
        public GameObject target;
        public List<MeshRenderer> arrowMR;
        public float speed = 1;

        public float minX;
        public float maxX;
        public float growingSpeed;

        public bool isGrowing = true;

        void Awake()
        {
            VuforiaARController.Instance.RegisterVuforiaInitializedCallback(OnVuforiaInitialized);
        }

        void OnVuforiaInitialized()
        {
            var deviceTracker = TrackerManager.Instance.InitTracker<PositionalDeviceTracker>();
            deviceTracker.Start();
        }

        private void Start()
        {
            arrowMR = this.GetComponentsInChildren<MeshRenderer>().ToList();
        }

        // Update is called once per frame
        void Update()
        {
            if (this.target == null && this.arrowMR.Any(am => am.material.color != Color.red))
            {
                foreach (var item in arrowMR)
                {
                    item.material.color = Color.red;
                }
            }

            if (this.target != null)
            {
                Vector3 targetDirection = target.transform.position - this.transform.position;
                this.transform.position = Camera.transform.position;
                this.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * speed, 0.0f));
            }
        }
    }
}