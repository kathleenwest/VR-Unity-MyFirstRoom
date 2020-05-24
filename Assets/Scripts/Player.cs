using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    FLY,
    FURNITURE,
    TRANSLATE,
    ROTATE,
    SCALE,
    FOOD,
    DRAG
}


public class Player : MonoBehaviour
{
    // Singleton design pattern
    public static Player instance;

    public InputMode activeMode = InputMode.NONE;

    [SerializeField]
    private float playerSpeed = 3.0f;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject forwardWall;
    public GameObject backWall;
    public GameObject ceiling;
    public GameObject floor;

    // Player Start Position
    public Vector3 playerStartPosition;

    // Active Furniture Prefab
    public Object activeFurniturePrefab;

    // Active Furniture Type
    public FurnitureType activeFurnitureType;

    // Active Food Prefab
    public Object activeFoodPrefab;

    // Active Food Type
    public FoodType activeFoodType;

    // Default Scalable Reference
    public Object tablePrefabObject;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }

        instance = this;

        // Set the Start Position
        playerStartPosition = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Player.instance.transform.position.z);

    }

    public void TryWalk()
    {
        if (Input.GetMouseButton(0) && activeMode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;
          
            Vector3 newPosition = transform.position + forward * Time.deltaTime * playerSpeed;

            // keep the player height
            //newPosition.y = Player.instance.transform.position.y;

            newPosition.y = Player.instance.playerStartPosition.y;

            //if (newPosition.x < rightWall.transform.position.x && newPosition.x > leftWall.transform.position.x &&
            //    newPosition.y < ceiling.transform.position.y && newPosition.y > floor.transform.position.y &&
            //    newPosition.z > backWall.transform.position.z && newPosition.z < forwardWall.transform.position.z)
            //{
            //    transform.position = newPosition;
            //}

            transform.position = newPosition;
        }

    } // end of method

    public void TryFly()
    {
        if (Input.GetMouseButton(0) && activeMode == InputMode.FLY)
        {
            Vector3 forward = Camera.main.transform.forward;

            Vector3 newPosition = transform.position + forward * Time.deltaTime * playerSpeed;

            // keep the player height
            //newPosition.y = Player.instance.transform.position.y;

            //if (newPosition.x < rightWall.transform.position.x && newPosition.x > leftWall.transform.position.x &&
            //    newPosition.y < ceiling.transform.position.y && newPosition.y > floor.transform.position.y &&
            //    newPosition.z > backWall.transform.position.z && newPosition.z < forwardWall.transform.position.z)
            //{
            //    transform.position = newPosition;
            //}

            transform.position = newPosition;
        }
    } // end of method


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
    }

    // Update is called once per frame
    void Update()
    {
        TryWalk();
        TryFly();
    }

} // end of class
