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
    public PlayerIndex playerIndex;

    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(playerIndex);
        
        if (state.IsConnected)
        {
            Vector3 movement = new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y);
            transform.position += movement * movementSpeed * Time.deltaTime;
        
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10.0f);
            }
        }
    }
}
