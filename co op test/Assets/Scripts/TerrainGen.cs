using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour{

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector2> uvs;

    public Texture2D terrainMap;

    Vector2 texSize;

    [SerializeField] private int heightScale = 1;
    

    void Start() {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        CreateShape();
        UpdateMesh();

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void CreateShape() {
        vertices = new List<Vector3>();
        uvs = new List<Vector2>();
        triangles = new List<int>();

        int vert = 0;

        for (int y = 0; y < terrainMap.height; y++) {
            for (int x = 0; x < terrainMap.width; x++) {
                //get heights of the 4 corners
                float[] Heights = { getHeight(x, y), getHeight(x, y + 1), getHeight(x + 1, y + 1), getHeight(x + 1, y) };

                //generate Vertecies
                for (int v = 0; v < 4; v++) {
                    vertices.Add(getVertexPosInClockwiseQuad(x, y, Heights[v], v));
                }

                //generate tri's
                triangles.Add(vert + 0);
                triangles.Add(vert + 1);
                triangles.Add(vert + 3);
                triangles.Add(vert + 1);
                triangles.Add(vert + 2);
                triangles.Add(vert + 3);

                //generate uv's
                switch (getTex(x, y)) {
                    case 0:
                        generateQuadUVs(new Vector2(0, 0), new Vector2(.5f, .5f));
                        break;
                    case 1:
                        generateQuadUVs(new Vector2(.5f, 0), new Vector2(1, .5f));
                        break;
                    case 2:
                        generateQuadUVs(new Vector2(0, .5f), new Vector2(.5f, 1));
                        break;
                    case 3:
                        generateQuadUVs(new Vector2(.5f, .5f), new Vector2(1, 1));
                        break;
                }

                vert += 4;
            }
        }
    }

    void UpdateMesh() {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
    }

    float getHeight(int x, int y) {
        return terrainMap.GetPixel(x, y).g * heightScale;
    }

    int getTex(int x,int y) {
        float r = terrainMap.GetPixel(x, y).r;

        if (r > 0 && r < .5f) {
            return 1; //grass 2
        }
        else if (r >= .5f && r < 1) {
            return 2; //rock
        }
        else if (r == 1) {
            return 3; //rock2
        }
        else return 0; //grass
    }

    Vector3 getVertexPosInClockwiseQuad(int x, int y, float cornerHeight, int cornerIndex) {
        Vector2 VertexPos = Vector2.zero;
        switch (cornerIndex) {
            case 0:
                VertexPos = new Vector2(x, y);
                break;
            case 1:
                VertexPos = new Vector2(x, y + 1);
                break;
            case 2:
                VertexPos = new Vector2(x + 1, y + 1);
                break;
            case 3:
                VertexPos = new Vector2(x + 1, y);
                break;
        }
        return new Vector3(VertexPos.x - terrainMap.width /2, cornerHeight, VertexPos.y - terrainMap.height /2);
    }

    void generateQuadUVs(Vector2 pos1, Vector2 pos2) {
        uvs.Add(new Vector2(pos1.x, pos1.y));
        uvs.Add(new Vector2(pos1.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos2.y));
        uvs.Add(new Vector2(pos2.x, pos1.y));
    }
}