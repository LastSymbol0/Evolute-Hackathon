using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatMass : MonoBehaviour
{
    public GameObject[] Mass;
    // Start is called before the first frame update
    void UpdateMass()
    {
        Mass = GameObject.FindGameObjectsWithTag("Mass"); 
    }


    public void AddMass(GameObject MassObject)
    {
        List<GameObject> MassList = new List<GameObject>();
        for (int i = 0; i < Mass.Length; i++)
        {
            MassList.Add(Mass[i]);
        }
        MassList.Add(MassObject);
        Mass = MassList.ToArray();

        MassSpawner.instance.AddMass(MassObject);
    }

    public void RemoveMass(GameObject MassObject)
    {
        List<GameObject> MassList = new List<GameObject>();
        for (int i = 0; i < Mass.Length; i++)
        {
            MassList.Add(Mass[i]);
        }
        MassList.Remove(MassObject);
        Mass = MassList.ToArray();

        MassSpawner.instance.RemoveMass(MassObject);
    }

    public void CheckMass()
    {
        for (int i = 0; i < Mass.Length; i++)
        {
            Transform m = Mass[i].transform;
            if(Vector2.Distance(transform.position, m.position) <= transform.localScale.x / 2)
            {
                RemoveMass(m.gameObject);
                PlayerEat();
                Destroy(m.gameObject);
            }
        }
    }
    
    void Start()
    {
        MassSpawner.instance.Players.Add(gameObject);
        UpdateMass();
        InvokeRepeating("CheckMass", 0, 0.1f);
    }

    public void PlayerEat()
    {
        transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
