using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class GazeableObject : MonoBehaviour
{
    public bool isTransformable = false;

    private int objectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    private Vector3 initialObjectRotation;
    private Vector3 initialPlayerRotation;
    private Vector3 initalObjectScale;

    private void Start()
    {
        //Disable this object's outline after initial setup.
        if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;

        }
    }


    /// <summary>
    /// OnGazeEnter
    /// When the user gazes at the object for the first time...
    /// Virtual: To be overridden by child derived objects
    /// User starts looking at something....
    /// </summary>
    /// <param name="hitInfo">The raycast information object </param>
    public virtual void OnGazeEnter(RaycastHit hitInfo)
    {
        Debug.Log("Gaze entered on " + gameObject.name);

        //If this object is furniture and the player is in a transformation mode, enable outline.
        if (isTransformable && (Player.instance.activeMode == InputMode.TRANSLATE || Player.instance.activeMode == InputMode.ROTATE || Player.instance.activeMode == InputMode.SCALE))
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = true;

        }

    } // end of method

    /// <summary>
    /// OnGaze
    /// When the user holds its gaze on an object...
    /// User keeps looking at something...
    /// Virtual: To be overridden by child derived objects
    /// </summary>
    /// <param name="hitInfo">The raycast information object </param>
    public virtual void OnGaze(RaycastHit hitInfo)
    {
        Debug.Log("Gaze hold on " + gameObject.name);

    } // end of method

    /// <summary>
    /// OnGazeExit
    /// When the user removes its gaze from a game object...
    /// User stops looking at something...
    /// Virtual: To be overridden by child derived objects
    /// </summary>
    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze exited on  " + gameObject.name);

        //If this object is furniture, disable outline. 
        if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;

        }

    } // end of method

    /// <summary>
    /// OnPress
    /// When the user clicks on the object...
    /// Virtual: To be overridden by child derived objects
    /// User presses the mouse or button....
    /// </summary>
    /// <param name="hitInfo">The raycast information object </param>
    public virtual void OnPress(RaycastHit hitInfo)
    {
        Debug.Log("Object pressed by user start...  " + gameObject.name);

        if (isTransformable)
        {
            // Get the Current Layer
            objectLayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            // Record initial rotations
            initialObjectRotation = transform.rotation.eulerAngles;
            initialPlayerRotation = Camera.main.transform.eulerAngles;

            // Record initial scale
            initalObjectScale = transform.localScale;
        }

    } // end of method

    /// <summary>
    /// OnHold
    /// When the user clicks on the object and holds it press...
    /// User holds the mouse click or button...
    /// Virtual: To be overridden by child derived objects
    /// </summary>
    /// <param name="hitInfo">The raycast information object </param>
    public virtual void OnHold(RaycastHit hitInfo)
    {
        Debug.Log("Object pressed by user held...  " + gameObject.name);

        if (isTransformable)
        {
            GazeTransform(hitInfo);
        }

    } // end of method

    /// <summary>
    /// OnRelease
    /// When the user releases the object...
    /// Virtual: To be overridden by child derived objects
    /// User releases the mouse click or button
    /// </summary>
    /// <param name="hitInfo">The raycast information object </param>
    public virtual void OnRelease(RaycastHit hitInfo)
    {
        Debug.Log("Object pressed by user released...  " + gameObject.name);

        if (isTransformable)
        {
            gameObject.layer = objectLayer;
        }

    } // end of method

    public virtual void GazeTransform(RaycastHit hitInfo)
    {
        // Call the correct transformation function
        switch (Player.instance.activeMode)
        {
            case InputMode.TRANSLATE:
                GazeTranslate(hitInfo);
                break;
            case InputMode.ROTATE:
                GazeRotate(hitInfo);
                break;
            case InputMode.SCALE:
                GazeScale(hitInfo);
                break;
            default:
                break;
        } // end of switch
    } // end of method


    public virtual void GazeTranslate(RaycastHit hitInfo)
    {
        // Move the object (position)

        // Only Do this if placing Furniture on Floor Only
        if (hitInfo.collider != null && hitInfo.collider.GetComponent<Floor>() && Player.instance.activeFoodType == FoodType.None)
        {

            Vector3 newPosition = hitInfo.point;

            // Get the object original y position
            newPosition.y = transform.position.y;

            // Update the object position
            transform.position = newPosition;

        }

        // Translating Food on Table Only
        else if (hitInfo.collider != null && hitInfo.collider.GetComponent<Table>() && Player.instance.activeFurnitureType == FurnitureType.None)
        {
            Vector3 newPosition = hitInfo.point;

            // Get the object original y position
            newPosition.y = transform.position.y;
           
            // Update the object position
            transform.position = newPosition;
        }

    } // end of method

    public virtual void GazeRotate(RaycastHit hitInfo)
    {
        // Change the object orientation (rotatation)
        // Change the object's orientation (rotation)

        float rotationSpeed = 5.0f;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;

        Vector3 currentObjectRotation = transform.rotation.eulerAngles;

        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        Vector3 newRotation = new Vector3(currentObjectRotation.x, initialObjectRotation.y + (rotationDelta.y * rotationSpeed), currentObjectRotation.z);

        transform.rotation = Quaternion.Euler(newRotation);
    }

    public virtual void GazeScale(RaycastHit hitInfo)
    {
        // Make the object bigger/smaller (scale)
        float scaleSpeed = 0.1f;
        float scaleFactor = 1;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        // If looking up
        if (rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f)
        {

            // If greater than 180, map it between 0 - 180
            if (rotationDelta.x > 180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }

            scaleFactor = 1.0f + Mathf.Abs(rotationDelta.x) * scaleSpeed;
        }
        // Looking Down
        else
        {
            if (rotationDelta.x < -180.0f)
            {
                rotationDelta.x = 360.0f + rotationDelta.x;
            }
            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationDelta.x) * (1 / scaleSpeed)) / 180.0f);
        }

        transform.localScale = scaleFactor * initalObjectScale;

    } // end of method

} // end of class
