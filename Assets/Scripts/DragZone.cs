using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : GazeableObject
{
    private VRCanvas parentPanel;
    private Transform originalParent;
    private InputMode savedInputMode = InputMode.NONE;


    // Start is called before the first frame update
    void Start()
    {
        parentPanel = GetComponentInParent<VRCanvas>();
    }

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform;

        savedInputMode = Player.instance.activeMode;
        Player.instance.activeMode = InputMode.DRAG;

    }

    public override void OnRelease(RaycastHit hitInfo)
    {
        base.OnRelease(hitInfo);

        parentPanel.transform.parent = originalParent;
        Player.instance.activeMode = savedInputMode;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
