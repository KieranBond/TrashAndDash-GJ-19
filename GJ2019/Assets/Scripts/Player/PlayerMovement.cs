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
    public float boostTime = 5;

    public bool InvertX = false;
    public bool InvertY = false;

    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(playerIndex);
        
        if (state.IsConnected)
        {
            Vector3 movement = new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y);
<<<<<<< HEAD
            //speed = movementSpeed;
=======
>>>>>>> master
            if(InvertX)
            {
                movement.x *= -1;
            }
            if(InvertY)
            {
                movement.z *= -1;
            }

<<<<<<< HEAD
            transform.position += movement * speed * Time.deltaTime;

=======
            transform.position += movement * movementSpeed * Time.deltaTime;
        
>>>>>>> master
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10.0f);
            }

            if(state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                if(boostTime >= 0)
                {
                    boostTime -= Time.deltaTime;
                    speed = boostSpeed;
                }
                else if(boostTime <= 0 || boostTime != 5)
                {
                    speed = movementSpeed;
                    boostTime += Time.deltaTime;
                }
            }
        }
    }
}
