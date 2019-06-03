using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerTrashCollect : MonoBehaviour
{
    [SerializeField]
    GameObject[] trash;
    [SerializeField]
    GameObject playerBarge;

    [SerializeField]
    int maxTrashCount = 3;

    GameObject trashSpawnZone;

    int index;

    bool enterDropOff = false;

    GamePadState prevState;
    GamePadState state;

    // Start is called before the first frame update
    void Start()
    {
        trash = new GameObject[maxTrashCount];
        trashSpawnZone = GameObject.Find("TrashSpawnZone");
    }

    // Update is called once per frame
    void Update()
    {
        //if (state.IsConnected)
        //{
            if (enterDropOff)
            {
                prevState = state;
                state = GamePad.GetState(GetComponent<PlayerMovement>().playerIndex);
                if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
                {
                    for (int i = 0; i < trash.Length; i++)
                    {
                        if (trash[i] != null)
                        {
                            GetComponent<PlayerScore>().InceremtnScore(1);
                            Destroy(trash[i]);
                            trash[i] = null;
                        }
                    }
                    index = 0;
                }
            }
        //}
    }

    public bool AddTrash(GameObject aTrash)
    {
        if (index < maxTrashCount)
        {
            trash[index] = aTrash;
            index++;

            return true;
        }
        return false;
    }

    public void DropTrash()
    {
        if(index > 0)
        {
            trash[index - 1].transform.SetParent(trashSpawnZone.transform);
            trash[index - 1].SetActive(true);
            trash[index - 1] = null;
            index--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BargeDropOff" && playerBarge == other.gameObject)
        {
            enterDropOff = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BargeDropOff" && playerBarge == other.gameObject)
        {
            enterDropOff = false;
        }
    }
}
