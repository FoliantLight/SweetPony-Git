using System;
using UnityEngine;

[System.Serializable]
public class Vector2Int {
    [SerializeField]
    private int m_row;
    [SerializeField]
    private int m_column;

    public Vector2Int(int row, int column) {
        m_row = row;
        m_column = column;
    }

    public int row {
        get { return m_row; }
        set { m_row = value; }
    }

    public int column {
        get { return m_column; }
        set { m_column = value; }
    }

    public override bool Equals(object o) {
        if(o is Vector2Int) {
            Vector2Int v = (Vector2Int)o;
            if(v != null) {
                if(m_row == v.m_row && m_column == v.m_column) {
                    return true;
                }
            }
        }
        return false;
    }

    public override int GetHashCode() {
        return 0;
    }

    public override string ToString() {
        return "(" + m_row + ", " + m_column + ")";
    }
}
