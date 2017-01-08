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
        gameObject.name = "Block - " + line + "," + column;
        GetComponent<SpriteRenderer>().sprite = m_parent.getSprite(line, column);
        if (GetComponent<SpriteRenderer>().sprite == null) gameObject.SetActive(false);
    }

    public bool OnPlayerActionMe() {
        return false; //TODO: m_parent.doAction(line, column);
    }

}
