using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHardLevel : MonoBehaviour
{
    private float VOXEL_SIZE = 0.02f;
    private int VOXELS_PER_REGION = 4;

    // Start is called before the first frame update
    void Awake()
    {
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void buildTopLeft()
    {
        for (int i = 0; i > -VOXELS_PER_REGION; i--)
        {
            for (int j = 0; j < VOXELS_PER_REGION; j++)
            {
                for (int k = 0; k < VOXELS_PER_REGION; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i > -(VOXELS_PER_REGION / 2) && j < VOXELS_PER_REGION / 2 && k < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Quitar";
                    }
                    else
                    {
                        cube.name = "Dejar";
                    }
                }
            }
        }
    }

    private void buildTopRight()
    {
        for (int i = 0; i < VOXELS_PER_REGION; i++)
        {
            for (int j = 0; j < VOXELS_PER_REGION; j++)
            {
                for (int k = 0; k < VOXELS_PER_REGION; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i < VOXELS_PER_REGION / 2 && j < VOXELS_PER_REGION / 2 && k < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Quitar";
                    }
                    else
                    {
                        cube.name = "Dejar";
                    }
                }
            }
        }
    }

    private void buildBottomRight()
    {
        for (int i = 0; i < VOXELS_PER_REGION; i++)
        {
            for (int j = 0; j > -VOXELS_PER_REGION; j--)
            {
                for (int k = 0; k < VOXELS_PER_REGION; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i < VOXELS_PER_REGION / 2 && j > -(VOXELS_PER_REGION / 2) && k < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Quitar";
                    }
                    else
                    {
                        cube.name = "Dejar";
                    }
                }
            }
        }
    }

    private void buildBottomLeft()
    {
        for (int i = 0; i > -VOXELS_PER_REGION; i--)
        {
            for (int j = 0; j > -VOXELS_PER_REGION; j--)
            {
                for (int k = 0; k < VOXELS_PER_REGION; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i > -(VOXELS_PER_REGION / 2) && j > -(VOXELS_PER_REGION / 2) && k < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Quitar";
                    }
                    else
                    {
                        cube.name = "Dejar";
                    }
                }
            }
        }
    }

    public void generate() {
        buildTopLeft();
        buildTopRight();
        buildBottomRight();
        buildBottomLeft();
    }
}
