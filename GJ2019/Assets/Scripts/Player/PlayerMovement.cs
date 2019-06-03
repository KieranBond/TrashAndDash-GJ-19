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
    public float speed;
    public PlayerIndex playerIndex;
    public Rigidbody rigidbody;
    public float boostTime;
    private bool canSpeedBoost;
    private float acceleration = 1;
    public bool InvertX = false;
    public bool InvertY = false;


    private Vector3 newPosition;
    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(playerIndex);
        
        if (state.IsConnected)
        {
            Vector3 movement = new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y);
            //speed = movementSpeed;
            if(InvertX)
            {
                movement.x *= -1;
            }
            if(InvertY)
            {
                movement.z *= -1;
            }
            if(state.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.Y == 0 && speed >= movementSpeed)
            {
               // transform.position += movement * (speed -= acceleration * Time.deltaTime) * Time.deltaTime;
                speed -= 5.0f * Time.deltaTime;//movement * (speed -= acceleration * Time.deltaTime) * Time.deltaTime;
                //rigidbody.velocity += boostSpeed * Vector3.forward;
            }
            else if(speed <= 10)
            {
                speed += acceleration * Time.deltaTime;
                //transform.position += movement * (speed += acceleration * Time.deltaTime) * Time.deltaTime;
            }
            else
            {
                speed = 10;
                //transform.position += movement * speed * Time.deltaTime;
            }
            newPosition = transform.position + -transform.up * speed * Time.deltaTime;
            

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10.0f);
                transform.rotation = Quaternion.Euler(-90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }


            if (state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                if (canSpeedBoost == false && boostTime >= 2)
                {
                    canSpeedBoost = true;
                }
            }

            if (canSpeedBoost)
            {
                if (boostTime >= 0)
                {
                    boostTime -= Time.deltaTime;
                    speed = boostSpeed;
                }
                else
                {
                    canSpeedBoost = false;
                }
            }
            if(!canSpeedBoost && boostTime <= 2)
            {
                speed = movementSpeed;
                boostTime += Time.deltaTime;
            }

        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position += -transform.up * speed * Time.deltaTime);
    }
}
