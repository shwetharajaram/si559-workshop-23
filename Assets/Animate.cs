using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{

    [Header("Animate Position")]
    public bool animatePosition = false;
    public float moveSpeed = 1.0f;
    //public Vector3 initialPosition = Vector3.zero;
    private Vector3 initialPosition;
    public Vector3 targetPosition = new Vector3(0.0f, 0.0f, 10.0f);

    [Header("Animate Rotation")]
    public bool animateRotation = false;
    public float rotationSpeed = 50.0f;
    public Vector3 rotationAxis = Vector3.up; // Adjust this in the Inspector to specify the rotation axis.

    [Header("Animate Scale")]
    public bool animateScale = false;
    public Vector3 targetScale = new Vector3(5.0f, 5.0f, 5.0f); // The target scale for the animation.
    public float duration = 2.0f; // The duration of the scale animation in seconds.

    private float journeyLength;
    private Vector3 initialScale;
    private float startTime;


    private void OnEnable()
    {
        initialPosition = this.transform.localPosition;
        journeyLength = Vector3.Distance(initialPosition, targetPosition);
        initialScale = this.transform.localScale;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (animatePosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            this.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
        }
        if (animateRotation)
        {
            // Rotate the object continuously based on the defined rotationSpeed and rotationAxis.
            this.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
        if (animateScale)
        {
            // Calculate the progress of the animation based on time
            float progress = (Time.time - startTime) / duration;
            // Make sure progress stays within [0, 1] range
            progress = Mathf.Clamp01(progress);
            // Interpolate between the initial scale and the target scale
            Vector3 newScale = Vector3.Lerp(initialScale, targetScale, progress);
            // Apply the new scale to the object
            transform.localScale = newScale;
        }
    }
}