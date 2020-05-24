using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour
{
    // Holds the Game Reticle
    public GameObject reticle;

    // Color of the Reticle when active
    public Color activeReticleColor = Color.blue;

    // Color of the Reticle when inactive
    public Color inactiveReticleColor = Color.white;

    // The game object being gazed at
    private GazeableObject currentGazeObject;

    // The game object that is selected
    private GazeableObject currentSelectedObject;

    // The last Raycast hit info
    private RaycastHit lastHit;

    // test


    // Start is called before the first frame update
    void Start()
    {
        // Set the Reticle to inactive color
        SetReticleColor(inactiveReticleColor);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGaze();
        CheckForInput(lastHit);
    }

    public void ProcessGaze()
    {
        // Hit Info for Raycast hit
        RaycastHit hit;

        // Raycast
        Ray ray = new Ray(transform.position, transform.forward);

        // Draws a Visual of the RayCast for Development Purposes
        Debug.DrawRay(ray.origin, ray.direction * 100);

        // If the Ray hits an object
        if(Physics.Raycast(ray, out hit))
        {
            // do something

            // check if the object is interactable
            // get the gameobject from the hitinfo
            GameObject hitObj = hit.collider.gameObject;

            // get the gazeable object from the hit object
            GazeableObject gazeObj = hitObj.GetComponentInParent<GazeableObject>();

            // Object has a gazeable component
            if(gazeObj != null)
            {
                // check if this is a new object (first time looking)

                // set the reticle color
                // Check if the object we are looking at is the same object
                if(gazeObj != currentGazeObject)
                {
                    // Clear the previous gaze object
                    ClearCurrentObject();

                    // Set the current gaze object reference to this object
                    currentGazeObject = gazeObj;

                    // Process the On Gaze Enter method on the object
                    currentGazeObject.OnGazeEnter(hit);

                    // Set the reticle to active
                    SetReticleColor(activeReticleColor);

                }
                else
                {
                    // Object is Being Starred At from previous frame
                    currentGazeObject.OnGaze(hit);
                }
            }
            // Object is not gazeable
            else
            {
                ClearCurrentObject();
            }

            // Set the Raycast hit history 
            lastHit = hit;

        } // end of if statement
        // Clear Current Game Object
        else
        {
            ClearCurrentObject();
        }

    } // end of method

    private void SetReticleColor(Color color)
    {
        // Set color of the reticle
        reticle.GetComponent<Renderer>().material.SetColor("_Color", color);

    } // end of method

    private void CheckForInput(RaycastHit hit)
    {
        // check for down for the VR cardboard button press
        if (Input.GetMouseButtonDown(0) && currentGazeObject != null)
        {
            currentSelectedObject = currentGazeObject;
            currentSelectedObject.OnPress(hit);
        }

        // check for hold
        else if(Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnHold(hit);
        }

        // check for release
        else if(Input.GetMouseButtonUp(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnRelease(hit);
            currentSelectedObject = null;
        }

    } // end of method

    private void ClearCurrentObject()
    {
        if(currentGazeObject != null)
        {
            // Call the Gaze Exit Method on the object
            currentGazeObject.OnGazeExit();

            // Set the reticle color to inactive
            SetReticleColor(inactiveReticleColor);

            // Set the object reference to null
            currentGazeObject = null;
        }
    } // end of method


} // end of class
