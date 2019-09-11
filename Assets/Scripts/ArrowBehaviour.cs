using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.imie.geocaching
{
    public class ArrowBehaviour : MonoBehaviour
    {
        public GameObject Camera;
        public GameObject target;
        public float speed;

        public float minX;
        public float maxX;
        public float growingSpeed;

        public bool isGrowing = true;

        // Update is called once per frame
        void Update()
        {
            Vector3 targetDirection = target.transform.position - this.transform.position;
            this.transform.position = Camera.transform.position;
            this.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * speed, 0.0f));
            //this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y + Time.deltaTime, this.transform.rotation.z, this.transform.rotation.w);

            // Change size
            if (this.transform.localScale.x < maxX && isGrowing)
            {
                isGrowing = true;
                this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime / growingSpeed, this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                isGrowing = false;
            }

            if (this.transform.localScale.x > minX && !isGrowing)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - Time.deltaTime / growingSpeed, this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                isGrowing = true;
            }
        }
    }
}