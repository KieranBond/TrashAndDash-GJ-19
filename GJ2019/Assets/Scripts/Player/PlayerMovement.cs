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
    [SerializeField]
    private float acceleration = 1;
    [SerializeField]
    private float deceleration = 10;
    [SerializeField]
    private float maxSpeed = 15;
    public bool InvertX = false;
    public bool InvertY = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Vector3 newPosition;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        LevelTimer.Instnace.PlayAgain += OnPlayAgain;
    }

    // Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(playerIndex);
        
        if (state.IsConnected)
        {
            Vector3 movement = MovementRelativeToCamera(new Vector3(state.ThumbSticks.Left.X, 0, state.ThumbSticks.Left.Y));
            //speed = movementSpeed;
            if(InvertX)
            {
                movement.x *= -1;
            }
            if(InvertY)
            {
                movement.z *= -1;
            }
            if(state.ThumbSticks.Left.X == 0 && state.ThumbSticks.Left.Y == 0 && speed >= movementSpeed || state.Buttons.B == ButtonState.Pressed && speed >= movementSpeed)
            {
               // transform.position += movement * (speed -= acceleration * Time.deltaTime) * Time.deltaTime;
                speed -= deceleration * Time.deltaTime;//movement * (speed -= acceleration * Time.deltaTime) * Time.deltaTime;
                //rigidbody.velocity += boostSpeed * Vector3.forward;
            }
            else if(speed <= maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
                //transform.position += movement * (speed += acceleration * Time.deltaTime) * Time.deltaTime;
            }
            else
            {
                speed = maxSpeed;
                //transform.position += movement * speed * Time.deltaTime;
            }
            //newPosition = transform.position + -transform.up * speed * Time.deltaTime;
            

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
                speed = maxSpeed;
                boostTime += Time.deltaTime;
            }

        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position += -transform.up * speed * Time.deltaTime);
    }

    public void AttackAnimFin()
    {
        GetComponentInChildren<PlayerHitController>().SetCollider(false);
    }

    public string Colour()
    {
        switch (playerIndex)
        {
            case PlayerIndex.One:
                return "Blue";
            case PlayerIndex.Two:
                return "Red";
            case PlayerIndex.Three:
                return "Green";
            case PlayerIndex.Four:
                return "Yellow";
        }
        return "";
    }

    void OnPlayAgain()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        speed = 0;

    }

    public static Vector3 MovementRelativeToCamera(Vector3 a_input)
    {
        Camera cam = Camera.main;
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
        return (a_input.x * camRight + a_input.z * camForward);
    }
}
