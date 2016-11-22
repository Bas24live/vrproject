using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    private bool[,] _open;
    private int _xSize;
    private int _ySize;

    public void RunInitialization() {
        _xSize = DungeonGenerator._dungeon.GetLength(0);
        _ySize = DungeonGenerator._dungeon.GetLength(1);
        InitializeOpen();
    }

    private void InitializeOpen()
    {
        _open = new bool[_xSize, _ySize];
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                switch (DungeonGenerator._dungeon[x, y])
                {
                    case Tile.Wall:
                        _open[x, y] = false;
                        break;
                    default:
                        _open[x, y] = true;
                        break;
                }
            }
        } 
    }

    public bool SpawnCenter(GameObject gameObject, Transform parent, int minSize, int maxSize)
    {
        Vector2 pos = new Vector2();
        if (GetRoomCenter(ref pos, minSize, maxSize))
        {
            SpawnGameObject(pos, gameObject, parent);
            _open[(int)pos.x, (int)pos.y] = false;
            return true;
        }
        return false;
    }

    public bool SpawnCenter(GameObject gameObject, Transform parent)
    {
        Vector2 pos = new Vector2();
        if (GetRoomCenter(ref pos))
        {
            SpawnGameObject(pos, gameObject, parent);
            _open[(int)pos.x, (int)pos.y] = false;
            return true;
        }
        return false;
    }

    public bool SpawnRandom(GameObject gameObject, Transform parent, int minSize, int maxSize)
    {
        Vector2 pos = new Vector2();
        if (GetRoom(ref pos, minSize, maxSize))
        {
            SpawnGameObject(pos, gameObject, parent);
            _open[(int)pos.x, (int)pos.y] = false;
            return true;
        }
        return false;
    }

    public bool SpawnRandom(GameObject gameObject, Transform parent) {
        Vector2 pos = new Vector2();
        if (GetRoom(ref pos)) {
            SpawnGameObject(pos, gameObject, parent);
            _open[(int)pos.x, (int)pos.y] = false;
            return true;
        }
        return false;
    }

    public bool SpawnRandom(GameObject gameObject, Transform parent, out GameObject clone)
    {
        Vector2 pos = new Vector2();
        if (GetRoom(ref pos))
        {
            clone = SpawnGameObject(pos, gameObject, parent);
            _open[(int)pos.x, (int)pos.y] = false;
            return true;
        }
        clone = new GameObject();
        return false;
    }

    public bool GetRoom(ref Vector2 pos)
    {
        ShuffleList(ref DungeonGenerator._rooms);
        foreach (var r in DungeonGenerator._rooms)
            if (GetRandomTile(r, ref pos))
                return true;
        return false;
    }

    private bool GetRoomCenter(ref Vector2 pos)
    {
        ShuffleList(ref DungeonGenerator._rooms);
        foreach (var r in DungeonGenerator._rooms)
            if (IsOpen(r.center))
            {
                pos = r.center; 
                return true;
            }
        return false;
    }

    private bool GetRoom(ref Vector2 pos, int minSize, int maxSize)
    {
        ShuffleList(ref DungeonGenerator._rooms);
        foreach (var r in DungeonGenerator._rooms)
            if (r.width >= minSize && r.width <= maxSize && r.height >= minSize && r.height <= maxSize)
                if(GetRandomTile(r, ref pos))
                    return true;
        return false;
    }

    private bool GetRoomCenter(ref Vector2 pos, int minSize, int maxSize)
    {
        ShuffleList(ref DungeonGenerator._rooms);
        foreach (var r in DungeonGenerator._rooms)
            if (IsOpen(r.center))
                if (r.width >= minSize && r.width <= maxSize && r.height >= minSize && r.height <= maxSize)
                {
                    pos = r.center; 
                    return true;
                }
        return false;
    }

    private void ShuffleList<T>(ref List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private bool GetRandomTile(Rect room, ref Vector2 pos)
    {
        List<Vector2> tiles = RoomTilesToList(room);
        ShuffleList(ref tiles);
        foreach (Vector2 t in tiles)
        {
            if (IsOpen(t))
            {
                pos = t;
                return true;
            }
        }
        return false;
    }

    private bool IsOpen(Vector2 pos)
    {
        return _open[(int) pos.x, (int) pos.y];
    }

    private List<Vector2> RoomTilesToList(Rect room)
    {
        List<Vector2> tiles = new List<Vector2>();
        for (int x = (int) room.xMin; x <= (int) room.xMax; x++)
        {
            for (int y = (int) room.yMin; y <= (int) room.yMax; y++)
            {
                tiles.Add(new Vector2(x, y));
            }
        }
        return tiles;
    }

    public GameObject SpawnGameObject(Vector2 pos, GameObject gameObject, Transform parent) {
        return Instantiate(gameObject, new Vector3(pos.x, gameObject.transform.position.y, pos.y), gameObject.transform.rotation, parent) as GameObject;
    }

    public GameObject SpawnGameObject(Vector3 pos, GameObject gameObject, Transform parent) {
        return Instantiate(gameObject, new Vector3(pos.x, gameObject.transform.position.y, pos.z), gameObject.transform.rotation, parent) as GameObject;
    }

    public Vector3 GetMapCenter() {
        return new Vector3(_xSize / 2, 0, _ySize / 2);
    }
}
