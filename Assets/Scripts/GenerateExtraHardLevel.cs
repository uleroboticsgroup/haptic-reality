using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateExtraHardLevel : MonoBehaviour
{
    private float VOXEL_SIZE = 0.01f;
    private int VOXELS_PER_REGION = 4;
    private int DEPTH = 6;

    private int hDispl;
    private int vDispl;

    // Start is called before the first frame update
    void Awake()
    {
        hDispl = Random.Range(-1, 2);

        if(hDispl == 0)
        {
            float r = Random.Range(0.0f, 1.0f);

            if(r < 0.5f)
            {
                vDispl = -1;
            }
            else
            {
                vDispl = 1;
            }
        }
        else
        {
            vDispl = Random.Range(-1, 2);
        }

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
                for (int k = 0; k < DEPTH; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i > -(VOXELS_PER_REGION / 2) + hDispl && j < (VOXELS_PER_REGION / 2) + vDispl && k < DEPTH / 2)
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
                for (int k = 0; k < DEPTH; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i < (VOXELS_PER_REGION / 2) + hDispl && j < (VOXELS_PER_REGION / 2) + vDispl && k < DEPTH / 2)
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
                for (int k = 0; k < DEPTH; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i < (VOXELS_PER_REGION / 2) + hDispl && j > -(VOXELS_PER_REGION / 2) + vDispl && k < DEPTH / 2)
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
                for (int k = 0; k < DEPTH; k++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                    cube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

                    if (i > -(VOXELS_PER_REGION / 2) + hDispl && j > -(VOXELS_PER_REGION / 2) + vDispl && k < DEPTH / 2)
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
