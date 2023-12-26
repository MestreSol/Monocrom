using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _startingPos, //This is the starting position of the sprites.
        _lengthOfSprite; //This is the length of the sprites.
    public float amountOfParallax; //This is amount of parallax scroll. 
    public Camera mainCamera; //Reference of the camera.



    private void Start()
    {
        //Getting the starting X position of sprite.
        _startingPos = transform.position.x;
        //Getting the length of the sprites.
        _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }



    private void Update()
    {
        Vector3 Position = mainCamera.transform.position;
        float Temp = Position.x * (1 - amountOfParallax);
        float Distance = Position.x * amountOfParallax;

        Vector3 NewPosition = new Vector3(_startingPos + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;

        if (Temp > _startingPos + (_lengthOfSprite / 2))
        {
            _startingPos += _lengthOfSprite;
        }
        else if (Temp < _startingPos - (_lengthOfSprite / 2))
        {
            _startingPos -= _lengthOfSprite;
        }
    }
}