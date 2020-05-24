using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour
{
    public GazeableButton currentActiveButton;

    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    public void SetActiveButton(GazeableButton currentButton)
    {
        if(currentActiveButton != null)
        {
            currentActiveButton.SetButtonColor(unselectedColor);
        }

        if (currentButton != null && currentActiveButton != currentButton)
        {
            currentActiveButton = currentButton;
            currentActiveButton.SetButtonColor(selectedColor);
        }
        else
        {
            Debug.Log("Resetting");
            currentActiveButton = null;
            Player.instance.activeMode = InputMode.NONE;
        }

    } // end of method

    public void LookAtPlayer()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 vecToPlayer = playerPos - transform.position;

        Vector3 lookAtPos = transform.position - vecToPlayer;

        transform.LookAt(lookAtPos);
    }

} // end of class
