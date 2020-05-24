using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType
{
    Chair,
    Table,
    Lamp,
    None
}

public class FurnitureButton : GazeableButton
{
    // Furniture Prefab
    public Object prefab;

    // Furniture Type
    public FurnitureType furnitureType;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // Set player mode to place furniture
        Player.instance.activeMode = InputMode.FURNITURE;

        // Set active furniture prefab to this furniture
        Player.instance.activeFurniturePrefab = prefab;

        // Set the active furniture type to this furniture typpe
        Player.instance.activeFurnitureType = furnitureType;

        // Select the active food type to none
        Player.instance.activeFoodType = FoodType.None;

    } // end of method

} // end of class
