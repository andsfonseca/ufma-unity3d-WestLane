using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {

    [Header("Phases")]
    public SceneObject[] phases;

    [Header("Initial Phase")]
    public SceneObject initialPhase;

    [Header("Phase Sequence")]
    public string phaseProgression;

    //Bloco que será instanciado
    public GameObject block;

    //Sequencia de Fases que serão instaciadas
    private List<GameObject> m_BlocksLineList;

    //Sequencia de Fases que serão instaciadas
    private List<SceneObject> m_PhaseProgressionList;

    //Iterator das Fases que serão instaciadas
    private int m_PhaseProgressionCurrent;

    //Fase Atual carregada
    private SceneObject m_currentPhase;

    //Proxima fase a ser carregada
    private SceneObject m_nextPhase;

    //Linha da Fase atual
    private int m_currentLine;

    //Booleana indicativa se as fases geradas serão randomicas
    private bool m_isRandom;

    //Blocos Registrados
    private Dictionary<string, SceneBlock> m_blockDict;

    private GameObject m_reversePointSpawn;

    ///<summary>
    ///Recupera a fase atual
    ///</summary>
    public SceneObject CurrentPhase {
        get {
            return m_currentPhase;
        }
    }

    void Start() {

        //Se a Fase inicial não foi carregada
        if (initialPhase == null) {
            Debug.Log("A fase inicial não foi cadastrada");
            gameObject.SetActive(false);
            return;
        }
        //Se o bloco não possui o Script de SceneBlock
        if (block.GetComponent<SceneBlock>() == null) {
            block.AddComponent<SceneBlock>();
        }

        //Se não existir fases carregadas
        if (phases.Length == 0) {
            Debug.Log("Nenhuma Fase Cadastrada");
        }

        //Recupera o PointSpawn
        m_reversePointSpawn = transform.GetChild(0).gameObject;
        m_reversePointSpawn.transform.position = new Vector2(transform.position.x * -1, transform.position.y);
        
        //Gera Listas e Dicionários
        m_BlocksLineList = new List<GameObject>();
        m_blockDict = new Dictionary<string, SceneBlock>();

        StartGeneration(8);
    }

    /// <summary>
    /// Gera um cenário inicial com o tamanho delimitado pelas linhas
    /// </summary>
    /// <param name="lines">Numero de linhas a ser instaciado</param>
	public void StartGeneration(int lines) {
        //Caso tenha Escrito Random, as fases serão aleatórias
        if (phaseProgression.Contains("random")) {
            m_isRandom = true;
            Debug.Log("RandomPhase Enabled");
        }
        // Caso não elas serão instanciado a partir da lista de fases registradas
        else {
            Debug.Log("RandomPhase Disabled");
            m_PhaseProgressionList = new List<SceneObject>();
            foreach (string s in phaseProgression.Split(',')) {
                m_PhaseProgressionList.Add(phases[int.Parse(s)]);
            }

            m_PhaseProgressionCurrent = 0;

        }

        m_currentLine = 0;
        m_currentPhase = initialPhase;
        m_nextPhase = getScene();
        for (int i = 0; i < lines; i++) {
            GenerateLine();
        }
    }

    /// <summary>
    /// Gera uma linha do cenário
    /// </summary>
	public void GenerateLine() {
        bool hasAInactiveObject = false;
        GameObject linesBlocks = null;
        foreach (GameObject go in m_BlocksLineList) {
            if (!go.activeSelf) {
                Debug.Log(!go.activeSelf);
                hasAInactiveObject = true;
                linesBlocks = go;
                break;
            }
        }

        if (hasAInactiveObject) {
            linesBlocks.SetActive(true);
            linesBlocks.transform.position = transform.position;
            for (int i = 0; i < 4; i++) {
                linesBlocks.transform.GetChild(i).position = new Vector3(transform.position.x + (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * i), transform.position.y, transform.position.z);
                linesBlocks.transform.GetChild(i).gameObject.GetComponent<SceneBlock>().setBlock(m_currentPhase, m_currentLine, i);
            }
            for (int i = 3, j = 0; i >= 0; i--, j++) {
                linesBlocks.transform.GetChild(7-j).position = new Vector3(m_reversePointSpawn.transform.position.x - (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * j), m_reversePointSpawn.transform.position.y, m_reversePointSpawn.transform.position.z);
                linesBlocks.transform.GetChild(7-j).gameObject.GetComponent<SceneBlock>().setBlock(m_currentPhase, m_currentLine, i);
            }
        }
        else {
            linesBlocks = new GameObject();
            linesBlocks.AddComponent<SceneLineChild>();
            linesBlocks.transform.position = transform.position;
            m_BlocksLineList.Add(linesBlocks);
            linesBlocks.transform.parent = GameLogic.Instance.GameElements;
            for (int i = 0; i < 4; i++) {
                GameObject blockNew = Instantiate(block, new Vector3(transform.position.x + (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * i), transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                blockNew.transform.parent = linesBlocks.transform;
                blockNew.GetComponent<SceneBlock>().setBlock(m_currentPhase, m_currentLine, i);
                m_blockDict.Add(m_BlocksLineList.IndexOf(linesBlocks) + "-" + m_currentLine + "," + i, blockNew.GetComponent<SceneBlock>());
            }
            for (int i = 3, j = 0; i >= 0; i--, j++) {
                GameObject blockNew = Instantiate(block, new Vector3(m_reversePointSpawn.transform.position.x - (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * j), m_reversePointSpawn.transform.position.y, m_reversePointSpawn.transform.position.z), Quaternion.identity) as GameObject;
                blockNew.transform.parent = linesBlocks.transform;
                blockNew.GetComponent<SceneBlock>().setBlock(m_currentPhase, m_currentLine, i);
                m_blockDict.Add(m_BlocksLineList.IndexOf(linesBlocks) + "-R" + m_currentLine + "," + i, blockNew.GetComponent<SceneBlock>());
            }


            linesBlocks.name = "LineBlock - " + m_BlocksLineList.IndexOf(linesBlocks);

        }
        transform.Translate(0, block.GetComponent<SpriteRenderer>().sprite.bounds.size.y, 0);
        Foward();
    }
    /// <summary>
    /// Avança de Cena e arruma o cenário para uma próxima Cena
    /// </summary>
    private void getNextScene() {
        m_currentPhase = m_nextPhase;
        Debug.Log(m_currentPhase.gameObject.name);
        m_nextPhase = getScene();
        m_currentLine = 0;
    }

    private void Foward() {
        if (m_currentLine < m_currentPhase.lanes.Length-1)
            m_currentLine++;
        else
            getNextScene();
    }

    /// <summary>
    /// Recupera Uma SceneObject Setado pelo atributos públicos do SceneGenerator
    /// </summary>
    /// <returns>Retorna um SceneObject com uma Cena definida</returns>
    private SceneObject getScene() {
        SceneObject aux;

        //Se estiver o random como true, Pega uma fase aleatória
        if (m_isRandom) {
            aux = phases[AndsLib.Util.Random.Range(0, phases.Length)];
        }
        //Caso não
        else {
            //Se o current já estiver ultrapassado o limite, ele volta a 0
            if (m_PhaseProgressionCurrent > (m_PhaseProgressionList.Count - 1)) {
                m_PhaseProgressionCurrent = 0;
            }

            //Pega a Scene Setada pelo Current 
            aux = m_PhaseProgressionList[m_PhaseProgressionCurrent];
        }

        return aux;
    }

    public float[] getLanePositions() {
        float[] positions = new float[4];
        for (int i = 0; i < 4; i++) {
            positions[i] = transform.position.x + (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * i);
        }
        return positions;
    }

    public float[] getReverseLanePositions() {
        float[] positions = new float[4];
        for (int i = 0, j=3 ; i < 4; i++, j--) {
            positions[j] = m_reversePointSpawn.transform.position.x - (block.GetComponent<SpriteRenderer>().sprite.bounds.size.x * i);
        }
        return positions;
    }

}



public class SceneLineChild: MonoBehaviour{
    
    void Update() {
        if (GameLogic.Instance.cameraMain.transform.position.y - transform.position.y > 10f) {
            gameObject.SetActive(false);
            GameLogic.Instance.sceneGenerator.GenerateLine();
        }
    }

    
}