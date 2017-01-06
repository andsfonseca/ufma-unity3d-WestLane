using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBlock : MonoBehaviour {

    private SceneObject m_parent;

    private int m_line;
    private int m_column;

    private SceneBlock[] sides;

    public SceneObject Parent {
        get {
            return m_parent;
        }
    }

    public int Line {
        get {
            return m_line;
        }
    }

    public int Column {
        get {
            return m_column;
        }
    }

    public void setBlock(SceneObject scObject, int line, int column) {
        m_parent = scObject;
        m_line = line;
        m_column = column;

        GetComponent<SpriteRenderer>().sprite = null; //TODO: m_parent.getSprite(line, column);
    }

    public bool OnPlayerTryTouchMe() {
        return false; //TODO: m_parent.OnPlayerTryTouch(line, column);
    }

    public bool OnPlayerActionMe() {
        return false; //TODO: m_parent.doAction(line, column);
    }

    public SceneBlock getAdjascent(string side) {
        switch (side) {
            case "left": 
                return sides[0];
            case "up":
                return sides[1];
            case "right":
                return sides[2];
            case "down":
                return sides[4];
            default:
                Debug.Log("String de lado incorreta");
                return null;
        }
    }
}
