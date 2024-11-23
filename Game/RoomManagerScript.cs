using System.Collections.Generic;
using UnityEngine;

public class RoomManagerScript : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] private int maxRooms;
    [SerializeField] private int minRooms;
    int roomWidth = 64;
    int roomHeight = 64;
    int gridSizeX = 10;
    int gridSizeY = 10;
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();
    private GameObject[,] roomArray;

    private int roomCount;

    private void Start()
    {
        roomQueue = new Queue<Vector2Int>();
        roomArray = new GameObject[gridSizeX,gridSizeY];

        Vector2Int InitialRoomIIndex = new Vector2Int(gridSizeX/2,gridSizeY/2);
        GenerateInitialRoom(InitialRoomIIndex);
        roomQueue.Enqueue(InitialRoomIIndex);

        roomArray[gridSizeX/2,gridSizeY/2].GetComponent<Room>().setCleared(true);
    }
    private void Update()
    {
        if(roomQueue.Count > 0 && roomCount < maxRooms)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int x = roomIndex.x;
            int y = roomIndex.y;
            GenerateRoom(new Vector2Int(x+1,y));
            GenerateRoom(new Vector2Int(x-1,y));
            GenerateRoom(new Vector2Int(x,y+1));
            GenerateRoom(new Vector2Int(x,y-1));

        }
        if(roomQueue.Count == 0 && roomCount < minRooms)
        {
            GameScript.Instance.RestartGame();
        }
    }

    private bool GenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        if (roomCount >=maxRooms)
            return false;
        if (Random.value <0.5f)
            return false;
        if (CountAdjacentRooms(roomIndex) > 1)
            return false;
        
        roomQueue.Enqueue(roomIndex);

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;
        
        roomCount++;
        if (roomCount < maxRooms){
            var newRoom = Instantiate(roomPrefab, GetPos(roomIndex), Quaternion.identity);
            roomArray[x,y] = newRoom;
            newRoom.name = $"room-{roomCount}";
            GenerateDoor(newRoom,x,y);
        }
        else
        {
            var bossRoom = Instantiate(bossRoomPrefab, GetPos(roomIndex), Quaternion.identity);
            roomArray[x,y] = bossRoom;
            bossRoom.name = $"room-boss";
            GenerateDoor(bossRoom,x,y);
        }



        return true;
    }
    private void GenerateInitialRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomCount++;
        var newRoom = Instantiate(roomPrefab, GetPos(roomIndex), Quaternion.identity);
        roomArray[x,y] = newRoom;
        newRoom.name = $"room-{roomCount}";
        newRoom.GetComponent<Room>().setCleared(true);
        GenerateDoor(newRoom,x,y);
    }
    private void GenerateDoor(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        Room leftRoomScript = GetRoomScript(x-1,y);
        Room rightRoomScript = GetRoomScript(x+1,y);
        Room topRoomScript = GetRoomScript(x,y+1);
        Room bottomRoomScript = GetRoomScript(x,y-1);

        if (leftRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (rightRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (topRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
        if (bottomRoomScript != null)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
    }
    Room GetRoomScript(int x,int y)
    {
        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return null;
        GameObject roomObject = roomArray[x,y];
        if (roomObject != null)
        {
            return roomObject.GetComponent<Room>();
        }
        return null;
        
    }
    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;
        if (x+1 >= gridSizeX-1){}
        else if(roomArray[x+1,y] != null) count++;

        if (x-1 <= 0){}
        else if(roomArray[x-1,y] != null) count++;

        if (y+1 >= gridSizeY-1){}
        else if(roomArray[x,y+1] != null) count++;

        if (y-1 <= 0){}
        else if(roomArray[x,y-1] != null) count++;

        return count;
    }
    private Vector3 GetPos(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX-gridSizeX/2), 
            roomHeight* (gridY - gridSizeY/2));
    }
    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color (0,1,1,0.05f);
        Gizmos.color = gizmoColor;

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y <gridSizeY; y++)
            {
                Vector3 position = GetPos(new Vector2Int(x,y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth,roomHeight,1));
            }
        }
    }
}
