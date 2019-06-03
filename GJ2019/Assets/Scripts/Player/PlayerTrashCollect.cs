using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollect : MonoBehaviour
{
    [SerializeField]
    GameObject[] trash;
    [SerializeField]
    GameObject playerBarge;

    GameObject trashSpawnZone;

    int index;

    bool enterDropOff = false;

    // Start is called before the first frame update
    void Start()
    {
        trash = new GameObject[5];
        trashSpawnZone = GameObject.Find("TrashSpawnZone");
    }

    // Update is called once per frame
    void Update()
    {
        if(enterDropOff)
        {
            if (Input.GetButtonDown("Fire1"))
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
    }

    public bool AddTrash(GameObject aTrash)
    {
        if (index < 5)
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
