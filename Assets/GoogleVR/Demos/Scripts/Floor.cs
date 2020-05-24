using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : GazeableObject
{
    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (Player.instance.activeMode == InputMode.TELEPORT)
        {
            Vector3 destination = hitInfo.point;

            // keep the player height
            destination.y = Player.instance.playerStartPosition.y;

            Player.instance.transform.position = destination;
        }

        else if(Player.instance.activeMode == InputMode.FURNITURE)
        {
            // Create the piece of furniture
            GameObject placedFurniture = GameObject.Instantiate(Player.instance.activeFurniturePrefab) as GameObject;

            // Set the position of the furniture
            placedFurniture.transform.position = hitInfo.point;

            // Pivot Point of the Lamp is Middle, Need to Adjust New Object Height
            if (Player.instance.activeFurnitureType == FurnitureType.Lamp)
            {
                // Transform the Height to Manual Set Value             
                Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.529f, hitInfo.point.z);
                placedFurniture.transform.position = newPosition;
            }

        } // end of else if


    } // end of method

} // end of class
