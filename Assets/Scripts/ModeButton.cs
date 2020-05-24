using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeableButton
{
    [SerializeField]
    private InputMode mode;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (parentPanel.currentActiveButton != null)
        {
            Player.instance.activeMode = mode;
        }
       
    } // end of method

} // end of class
