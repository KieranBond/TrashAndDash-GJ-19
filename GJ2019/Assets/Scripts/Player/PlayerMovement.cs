using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public enum Player
{
    PlayerOne,
    PlayerTwo,
    PlayerThree,
    PlayerFour
}

public class PlayerMovement : MonoBehaviour
{
    private GamePadState state;
    public float movementSpeed;
    public float boostSpeed;
    private float speed;
    public PlayerIndex playerIndex;
    private bool isSpeed;

    public bool InvertX = false;
    public bool InvertY = false;

    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(playerIndex);
        
        if (state.IsConnected)
        {
            Vector3 movement = new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y);
            if(InvertX)
            {
                movement.x *= -1;
            }
            if(InvertY)
            {
                movement.z *= -1;
            }

            transform.position += movement * movementSpeed * Time.deltaTime;
        
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10.0f);
            }

            if(state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                if(isSpeed)
                {
                    speed = boostSpeed;
                }
            }
            else
            {
                speed = movementSpeed;
            }
        }
    }

    private IEnumerator Wait()
    {
        speed = boostSpeed;
        yield return new WaitForSeconds(2);
    }
}
