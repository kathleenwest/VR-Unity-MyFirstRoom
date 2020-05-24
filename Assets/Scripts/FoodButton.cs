using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Beer,
    Chicken,
    Pie,
    None
}
public class FoodButton : GazeableButton
{
    // Furniture Prefab
    public Object prefab;

    // Furniture Type
    public FoodType foodType;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // Set player mode to place furniture
        Player.instance.activeMode = InputMode.FOOD;

        // Set active food prefab to this food
        Player.instance.activeFoodPrefab = prefab;

        // Set the active type to this food typpe
        Player.instance.activeFoodType = foodType;

        // Select the active furniture type to none
        Player.instance.activeFurnitureType = FurnitureType.None;

    } // end of method
}
