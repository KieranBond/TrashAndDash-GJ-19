using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShipMovement : MonoBehaviour
{
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform endPosition;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    bool isMoving = false;

    [SerializeField]
    GameObject[] spawPrefabs;

    [SerializeField]
    Transform spawnParent;

    [SerializeField]
    Transform spawnPosition;

    Coroutine c = null;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if(c == null)
            {
                c = StartCoroutine(DropTrash());
            }
            Vector3 dir = startPosition.position - endPosition.position;
            transform.position += -dir * movementSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, endPosition.position) < 0.1f)
            {
                StopCoroutine(c);
                c = null;
                isMoving = false;
            }
        }

        if(!isMoving && LevelTimer.Instnace.RemainningTime <= 60.0f && LevelTimer.Instnace.RemainningTime > 0.0f)
        {
            SetIsMoving();
        }
    }

    public void StartMoving()
    {
        SetIsMoving();
    }

    Vector3 RandomPositionInBounds(Bounds aBounds)
    {
        return new Vector3(
            Random.Range(aBounds.min.x, aBounds.max.x),
            Random.Range(aBounds.min.y, aBounds.max.y),
            Random.Range(aBounds.min.z, aBounds.max.z));
    }

    IEnumerator DropTrash()
    {
        bool drop = true;
        int index = 0;
        yield return new WaitForSeconds(4.0f);
        while (drop)
        {
            //drop things 
            int randomIndex = Random.Range(0, (spawPrefabs.Length - 1));
            GameObject go = Instantiate(spawPrefabs[randomIndex], spawnParent);
            go.transform.position = RandomPositionInBounds(spawnPosition.GetComponent<BoxCollider>().bounds);

            index++;
            if(index >= 10)
            {
                drop = false;
            }
            yield return new WaitForSeconds(0.35f);
        }
    }

    public void SetIsMoving()
    {
        isMoving = true;
        transform.position = startPosition.position;
    }
}
