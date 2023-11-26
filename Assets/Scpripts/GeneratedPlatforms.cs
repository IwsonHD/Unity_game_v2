using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField]
    private GameObject platformPrefab;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private bool isOn = false;


    private const int PLATFORMS_NUM = 8;
    private const int PLTFORM_RADIUS = 5;


    private GameObject[] platforms;
    private Vector3[] positions;
    private float[] arcPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn) 
        {
            for (int i = 0; i < PLATFORMS_NUM; i++)
            {
                arcPositions[i] += 0.001f;
                positions[i].x = (float)Math.Cos(arcPositions[i]) * PLTFORM_RADIUS + transform.position.x;
                positions[i].y = (float)Math.Sin(arcPositions[i]) * PLTFORM_RADIUS + transform.position.y;
                platforms[i].transform.position = Vector3.MoveTowards(platforms[i].transform.position, positions[i], speed * Time.deltaTime);
            }
		}

    }

	private void Awake()
	{
		platforms = new GameObject[PLATFORMS_NUM];
        positions = new Vector3[PLATFORMS_NUM];
        arcPositions= new float[PLATFORMS_NUM];
        for(int i = 0; i < PLATFORMS_NUM; i++)
        {
            arcPositions[i] = (float)(i * 2 * Math.PI / PLATFORMS_NUM);

			positions[i].x = (float)Math.Cos(arcPositions[i]) * PLTFORM_RADIUS + transform.position.x;
            positions[i].y = (float)Math.Sin(arcPositions[i]) * PLTFORM_RADIUS + transform.position.y;
            platforms[i] = Instantiate(platformPrefab, positions[i], Quaternion.identity);
            platforms[i].tag = "MovingPlatform";
            
		}



	}

    public void TurnOnOff()
    {
        isOn ^= true;
    }

}
