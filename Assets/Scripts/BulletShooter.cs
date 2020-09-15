using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    bool shootButtonPressed = false;
    const float BulletSpeed = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Shoot") > 0)
        {
            if (!shootButtonPressed)
            {
                shootButtonPressed = true;
                GameObject bullet = Instantiate<GameObject>(prefabBullet,
                                                            transform.position,
                                                            Quaternion.identity);
                Vector2 shipFacing = GetComponent<SpaceShip>().ThrustDirection;

                bullet.GetComponent<Rigidbody2D>().AddForce(shipFacing * BulletSpeed,
                                                            ForceMode2D.Impulse);
                AudioManager.Play(AudioClipName.PlayerShot);
            }
        }
        else
        {
            shootButtonPressed = false;
        }
    }
}
