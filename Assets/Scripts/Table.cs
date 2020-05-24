using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : GazeableObject
{

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (Player.instance.activeMode == InputMode.FOOD)
        {
            // Create the piece of food
            GameObject placedFood = GameObject.Instantiate(Player.instance.activeFoodPrefab) as GameObject;

            // Create the default table
            GameObject tablePrefab = GameObject.Instantiate(Player.instance.tablePrefabObject) as GameObject;

            // Get the current table object
            GameObject table = hitInfo.collider.GetComponent<Table>().gameObject;

            #region scaling

            // Get the scale of the current table object
            Vector3 currentTableScale = table.transform.localScale;

            // Get the Scale of Prefab Object
            Vector3 prefabTableScale = tablePrefab.transform.localScale;

            // Compute X Ratio
            float xScale = currentTableScale.x / prefabTableScale.x;

            // Compute Y ratio
            float yScale = currentTableScale.y / prefabTableScale.y;

            // Compute Z ratio
            float zScale = currentTableScale.z / prefabTableScale.z;

            // Create New Scale Vector
            Vector3 scaleVector = new Vector3(xScale, yScale, zScale);

            // Scales the Current Food Item Relative to Size of Current Table Size
            placedFood.transform.localScale = Vector3.Scale(scaleVector, placedFood.transform.localScale);

            #endregion scaling

            #region position adjustments

            // Table Y Pivot Point is on the Table
            Vector3 newPosition = new Vector3(hitInfo.point.x, table.transform.position.y, hitInfo.point.z);

            // Add the ScaledHeight Offset
            newPosition.y += 0.30f * yScale;

            // Precise Placements for On Dining Table
            switch (Player.instance.activeFoodType)
            {
                case FoodType.Beer:
                    newPosition.y += 0.05f *yScale;
                    break;
                case FoodType.Chicken:
                    newPosition.y += 0f;
                    break;
                case FoodType.Pie:
                    newPosition.y += 0f;
                    break;
            } // end of switch

            placedFood.transform.position = newPosition;

            #endregion position adjustments

        } // end of if

    } // end of method

} // end of class
