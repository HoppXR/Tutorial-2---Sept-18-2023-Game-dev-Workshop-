using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ladder : MonoBehaviour
{
    [SerializeField] private int _topTile;
    [SerializeField] private int _bottomTile;

    public Ladder(int topTile, int bottomTile)
    {
        _topTile = topTile;
        _bottomTile = bottomTile;
    }

    public int GetTopTile()
    {
        return _topTile;
    }

    public int GetBottomTile()
    {
        return _bottomTile;
    }
}
