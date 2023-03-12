using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallChunk : MonoBehaviour
{
    //chunk size

    //0 = air, 1 = land
    public int[] rangeX = new int[2];
    public int[] rangeZ = new int[2];
    public int[] rangeY = new int[2] { 0, TerrainChunk.chunkHeight };
    //public int[,] locs = new int[16, 16];


    // Start is called before the first frame update
    void Start()
    {
 
    }



    public void setRange(int[] rangex, int [] rangez)
    {
        rangeX = rangex;
        rangeZ = rangez;
    }

    readonly Vector2[] uvpat = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

    public void BuildMesh()
    {

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        Vector3[,,] points = new Vector3[2,2,2];
        foreach (int i in Enumerable.Range(0, 2))
            foreach (int j in Enumerable.Range(0, 2))
                foreach (int k in Enumerable.Range(0, 2))
                { points[i, j, k] = new Vector3(rangeX[i], rangeY[j], rangeZ[k]); }

        int[] idx = new int[3];
        foreach (int face in Enumerable.Range(0, 3))
        {
           
            int ax1 = (face + 1) % 3; int ax2 = (face + 2) % 3;
            for (int ud = 0; ud < 2; ud++)
            { //up and down
                if (face == 1 && ud == 1) continue;
                    idx[face] = ud;
                for (int c = 0; c < 2; c++)
                {
                    idx[ax1] = 0; idx[ax2] = 0;
                    verts.Add(points[idx[0], idx[1], idx[2]]);
                    idx[ax1] = 0; idx[ax2] = 1;
                    verts.Add(points[idx[0], idx[1], idx[2]]);
                    idx[ax1] = 1; idx[ax2] = 1;
                    verts.Add(points[idx[0], idx[1], idx[2]]);
                    idx[ax1] = 1; idx[ax2] = 0;
                    verts.Add(points[idx[0], idx[1], idx[2]]);
                }
                uvs.AddRange(uvpat);
                uvs.AddRange(uvpat);
                int tl = verts.Count - 8;
                tris.AddRange(new int[] { tl, tl + 1, tl + 2, tl, tl + 2, tl + 3,
                        tl+3+4,tl+2+4,tl+4,tl+2+4,tl+1+4,tl+4});
            }
        }
            //points[i,j,k]  = new Vector3(rangeX[i],rangeY[j],rangeZ[k]);
            //ax1 = (face + 1)%3; ax2 = (face+2)%3;
            //idx[face]=0;   idx[ax1] = 0; idx[ax2] = 0; //(0,1),(1,1),(1,0)..
            //idx[face]=1;   idx[ax1] = 0; idx[ax2] = 0; //(0,1),(1,1),(1,0)

            //verts.Add(points[idx[0],idx[1],idx[2]]);
            // k in 0, 1
            // for (int j in Enumerable.Range(0,2))  (foreach i in new int[]{j, (j+1)%2}
            //
            //Add( points[i,j,k]);
            //

            //
            //uvs.AddRange(uvpat);
            //uvs.AddRange(uvpat);
            //
            //for (int face in Enumerable.Range(0,2))  (1<<face)
            //verts.Add (new Vector3(points[]);

        Mesh mesh = new Mesh();

        //verts.Add(new Vector3(rangeX[0], 0, rangeZ[0]));
        //verts.Add(new Vector3(rangeX[1], 0, rangeZ[0]));
        //verts.Add(new Vector3(rangeX[1], 1, rangeZ[0]));
        //verts.Add(new Vector3(rangeX[0], 1, rangeZ[0]));
        //uvs.AddRange(uvpat);

        //verts.Add(new Vector3(rangeX[0], 0, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[1], 0, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[1], 1, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[0], 1, rangeZ[1]));
        //uvs.AddRange(uvpat);

        //verts.Add(new Vector3(rangeX[0], 0, rangeZ[0]));
        //verts.Add(new Vector3(rangeX[0], 0, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[0], 1, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[0], 1, rangeZ[0]));
        //uvs.AddRange(uvpat);

        //verts.Add(new Vector3(rangeX[1], 0, rangeZ[0]));
        //verts.Add(new Vector3(rangeX[1], 0, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[1], 1, rangeZ[1]));
        //verts.Add(new Vector3(rangeX[1], 1, rangeZ[0]));
        //uvs.AddRange(uvpat);

    

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

    }

}
