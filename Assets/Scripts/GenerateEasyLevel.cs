using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEasyLevel : MonoBehaviour
{
    private float VOXEL_SIZE = 0.01f;
    private int VOXELS_PER_REGION = 4;
    private int DEPTH = 6;

    // Start is called before the first frame update
    void Awake()
    {
        generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void buildTopLeft() {
        for(int i=0; i>-VOXELS_PER_REGION; i--) {
            for(int j=0; j<VOXELS_PER_REGION; j++) {
                for(int k=0; k<DEPTH; k++) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.GetComponent<Renderer>().material.color = new Color(1,0,0,1);
                    cube.name = "Dejar";
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);
                }
            }
        }
    }

    private void buildTopRight() {
        for(int i=0; i<VOXELS_PER_REGION; i++) {
            for(int j=0; j<VOXELS_PER_REGION; j++) {
                for(int k=0; k<DEPTH; k++) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) + (VOXEL_SIZE / 2), k * VOXEL_SIZE);

                    if (i < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Dejar";
                        cube.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                    }
                    else
                    {
                        cube.name = "Quitar";
                        cube.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
                    }
                }
            }
        }
    }

    private void buildBottomRight() {
        for(int i=0; i<VOXELS_PER_REGION; i++) {
            for(int j=0; j>-VOXELS_PER_REGION; j--) {
                for(int k=0; k<DEPTH; k++) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) + (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);

                    if (i < VOXELS_PER_REGION / 2)
                    {
                        cube.name = "Dejar";
                        cube.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                    }
                    else
                    {
                        cube.name = "Quitar";
                        cube.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
                    }
                }
            }
        }
    }

    private void buildBottomLeft() {
        for(int i=0; i>-VOXELS_PER_REGION; i--) {
            for(int j=0; j>-VOXELS_PER_REGION; j--) {
                for(int k=0; k<DEPTH; k++) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.GetComponent<Renderer>().material.color = new Color(1,0,0,1);
                    cube.name = "Dejar";
                    cube.tag = "Touchable";
                    cube.transform.parent = this.transform;
                    cube.transform.localScale = new Vector3(VOXEL_SIZE, VOXEL_SIZE, VOXEL_SIZE);
                    cube.transform.localPosition = new Vector3((i * VOXEL_SIZE) - (VOXEL_SIZE / 2), (j * VOXEL_SIZE) - (VOXEL_SIZE / 2), k * VOXEL_SIZE);
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
