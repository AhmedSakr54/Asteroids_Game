using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{

    float radiusOfColliderAttachedToShip;

    public float RadiusOfColliderAttachedToShip
    {
        get { return radiusOfColliderAttachedToShip; }
    }
    // Start is called before the first frame update
    void Start()
    {
        radiusOfColliderAttachedToShip = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBecameInvisible()
    {
        Vector2 currentPosition = transform.position;

        if (currentPosition.x + radiusOfColliderAttachedToShip > ScreenUtils.ScreenRight)
            currentPosition.x = ScreenUtils.ScreenLeft;
        else if (currentPosition.x - radiusOfColliderAttachedToShip < ScreenUtils.ScreenLeft)
            currentPosition.x = ScreenUtils.ScreenRight;


        else if (currentPosition.y + radiusOfColliderAttachedToShip > ScreenUtils.ScreenTop)
            currentPosition.y = ScreenUtils.ScreenBottom;
        else if (currentPosition.y - radiusOfColliderAttachedToShip < ScreenUtils.ScreenBottom)
            currentPosition.y = ScreenUtils.ScreenTop;

        transform.position = currentPosition;
    }
}
