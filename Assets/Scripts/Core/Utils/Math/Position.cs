using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Position {
    [SerializeField]
    private int column;
    [SerializeField]
    private int row;

    public Position(int col, int row) {
        this.column = col;
        this.row = row;
    }

    public int Column
    {
        get
        {
            return column;
        }
        set
        {
            column = value;
        }
    }

    public int Row
    {
        get
        {
            return row;
        }
        set
        {
            row = value;
        }
    }

    /*public List<Position> GetNeighbors() {
        return new List<Position>() {
            new Position(column - 1, row), new Position(column, row + 1), new Position(column, row + 2),
            new Position(column, row - 1), new Position(column + 1, row + 1), new Position(column, row - 2),
            new Position(column + 1, row - 1), new Position(column + 1, row)
        };
    */

    public Position NeighborTR() {
        if(row % 2 == 0) {
            return new Position(column + 1, row + 1);
        } else {
            return new Position(column, row + 1);
        }
    }

    public Position NeighborTL() {
        if (row % 2 == 0) {
            return new Position(column, row + 1);
        }
        else {
            return new Position(column - 1, row + 1);
        }
    }

    public Position NeighborBR() {
        if (row % 2 == 0) {
            return new Position(column + 1, row - 1);
        }
        else {
            return new Position(column, row - 1);
        }
    }

    public Position NeighborBL() {
        if (row % 2 == 0) {
            return new Position(column, row - 1);
        }
        else {
            return new Position(column - 1, row - 1);
        }
    }

    public Vector2 ToPoint(float width, float height) {
        var x = column * width - (row % 2 == 0 ? 0 : width / 2);
        var y = row * height / 2;
        return new Vector2(x, y);
    }

    static Position ToPosition(Vector2 point, float width, float height) {
        var r = point.y * 2 / height;
        var c = point.x / width + (r % 2 == 0 ? 0 : width / 2);
        return new Position((int)System.Math.Round(c), (int)System.Math.Round(r));
    }

    public override int GetHashCode() {
        return (column + "," + row).GetHashCode();
    }

    public override bool Equals(object obj) {
        if (obj is Position) {
            return ((Position)obj).column == this.column && ((Position)obj).row == this.row;
        }
        return false;
    }

    public override string ToString() {
        return column + "," + row;
    }

    public static Position operator+(Position pos1, Position pos2) {
        return new Position(pos1.column + pos2.column, pos1.row + pos2.row);
    }
}
