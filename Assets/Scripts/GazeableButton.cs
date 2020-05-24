using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GazeableButton : GazeableObject
{

    protected VRCanvas parentPanel;

    // Start is called before the first frame update
    void Start()
    {
        parentPanel = GetComponentInParent<VRCanvas>();
    } // end of method

    // Update is called once per frame
    void Update()
    {
        
    } // end of method

    public void SetButtonColor(Color buttonColor)
    {
        GetComponent<Image>().color = buttonColor;

    } // end of method

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (parentPanel != null)
        {
            parentPanel.SetActiveButton(this);
        }
        else
        {
            Debug.LogError("Button is not a child of a parent VRCanvas", this);
        }

    } // end of method

} // end of class
