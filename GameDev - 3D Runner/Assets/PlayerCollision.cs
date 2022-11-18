using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement pm;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name == "Game Over Block")
        {
            pm.enabled = false;
            Debug.Log("GAME OVER!");
        }
    }
}
