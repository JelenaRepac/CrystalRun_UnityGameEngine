using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public Vector3 lastPos;
    public float offset = 0.707f;

    private int roadCount = 0;

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, .5f);
    }

    public void CreateNewRoadPart()
    {

        Vector3 spawnPos;
        float chance = Random.Range(0, 100);

        if (chance < 50)
        {
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        }
        else
        {
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);
        }

        GameObject gameObject = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));//rotiramo

        lastPos = gameObject.transform.position;
        roadCount++;

        if (roadCount % 5 == 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
