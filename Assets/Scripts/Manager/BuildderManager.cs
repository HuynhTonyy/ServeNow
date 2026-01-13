using UnityEngine;

public class BuildderManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float verticalOffset;
    [Header("Ground")]
    [SerializeField] private int size;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject groundCollision;
    [SerializeField] private Transform groundTileContainer;
    [Header("Wall")]
    [SerializeField] private float wallOffset;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform wallContainer;

    
    void Start()
    {
        var colisionObj = Instantiate(groundCollision,Vector3.zero, Quaternion.identity,groundTileContainer);
        var colider = colisionObj.GetComponent<BoxCollider>();
        colider.center = new Vector3(height*size/2 -2,verticalOffset,width*size/2 - 2);
        colider.size =  new Vector3(height*size,0,width*size);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = groundPrefab;
                var position = new Vector3(y,0f,x) * size;
                var rotation = Quaternion.identity;
                var groundTile = Instantiate(tile, position, rotation, groundTileContainer);
                groundTile.transform.SetLocalPositionAndRotation(position+new Vector3(0,verticalOffset,0),Quaternion.identity);
                var offset = size/2+wallOffset;
                if(x == 0)
                {
                    var newPos = position + new Vector3(0,0,-offset);
                    var wall = Instantiate(wallPrefab, newPos, rotation, wallContainer);
                    wall.transform.localPosition += new Vector3(0,verticalOffset,0);
                }
                if(x == width-1)
                {
                    var newPos =  position + new Vector3(0,0,offset);
                    var wall = Instantiate(wallPrefab, newPos, rotation, wallContainer);
                    wall.transform.localPosition += new Vector3(0,verticalOffset,0);

                }
                if(y == 0)
                {
                    rotation = Quaternion.Euler(0,90,0);
                    var newPos =  position + new Vector3(-offset,0,0);
                    var wall = Instantiate(wallPrefab, newPos, rotation, wallContainer);
                    wall.transform.localPosition += new Vector3(0,verticalOffset,0);

                }
                if(y == height - 1)
                {
                    rotation = Quaternion.Euler(0,90,0);

                    var newPos =  position + new Vector3(offset,0,0);
                    var wall = Instantiate(wallPrefab, newPos, rotation, wallContainer);
                    wall.transform.localPosition += new Vector3(0,verticalOffset,0);

                }
            }
        }
    }
}
