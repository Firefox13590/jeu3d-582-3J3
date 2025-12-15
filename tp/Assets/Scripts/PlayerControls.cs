using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControls : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];
    public Vector3 playerPosAjust = Vector3.zero;
    public GameManager gameManager;

    [Header("Variables de test")]
    public int testCurrentPos = 0;

    [Header("Acces publique pour autres scripts")]
    public Vector3 targetPos = Vector3.zero, plannedRedirect = Vector3.zero;
    public bool allowTileChoice = false;

    // variables privées
    GameObject[] listeCases;
    int movesLeft;
    bool allowInput = true, allowMove = false; // gestion des permissions

    // événements statiques
    public static event Action OnTurnEnd;
    public static event Action OnTileChoiceEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // abonnement aux évènements
        Case.OnTileChoiceStart += StartTileSelection;
        Case.OnTileRedirect += TileRedirect;

        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));

        for(int i = 0; i < playerObjects.Length; i++)
        {
            gameSettings.Players[i].CurrentPos = testCurrentPos;
            playerObjects[i].transform.position = listeCases[gameSettings.Players[i].CurrentPos].transform.position + playerPosAjust;
        }
    }

    private void OnDestroy()
    {
        // désabonnement aux évènements
        Case.OnTileChoiceStart -= StartTileSelection;
        Case.OnTileRedirect -= TileRedirect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(gameSettings.Players[GameManager.playerTurn].Controls.Action) && allowInput)
        {
            GetMovesLeft();
        }

        if(allowMove)
        {
            MovePlayer();
        }

        if (allowTileChoice)
        {
            ChooseTile();
        }
    }



    /// <summary>
    /// Obtient un nombre aléatoire de mouvements restants entre 0 et 7 inclus.
    /// </summary>
    void GetMovesLeft()
    {
        movesLeft = Random.Range(0, 8);
        //Debug.Log("starting moves left: " + movesLeft);

        allowMove = true;
        allowInput = false;
    }

    /// <summary>
    /// Déplace le joueur en fonction des mouvements restants.
    /// </summary>
    void MovePlayer()
    {
        if (movesLeft > 0)
        {
            // calcul de la destiination et la distance tant qu'il reste des mouvements
            if (targetPos == Vector3.zero)
            {
                targetPos = listeCases[gameSettings.Players[GameManager.playerTurn].CurrentPos + 1].transform.position + playerPosAjust;
            }
            float distance = Vector3.Distance(playerObjects[GameManager.playerTurn].transform.position, targetPos);

            if (distance > 0)
            {
                // tant que la destination n'est pas atteinte, le joueur continue de se déplacer
                playerObjects[GameManager.playerTurn].transform.position = Vector3.MoveTowards(playerObjects[GameManager.playerTurn].transform.position, targetPos, .1f);
            }
            else
            {
                // sinon, les mouvements restants sont décrémentés et la position actuelle du joueur est mise à jour
                gameSettings.Players[GameManager.playerTurn].CurrentPos++;
                Debug.Log($"new CurrentPos: {gameSettings.Players[GameManager.playerTurn].CurrentPos}    Vector3: {playerObjects[GameManager.playerTurn].transform.position}");
                movesLeft--;
                //Debug.Log("current moves left: " + movesLeft);

                if(plannedRedirect != Vector3.zero)
                {
                    targetPos = plannedRedirect;
                    plannedRedirect = Vector3.zero;
                }
                else
                {
                    targetPos = Vector3.zero;
                }
            }
        }
        else
        {
            OnTurnEnd.Invoke();

            allowMove = false;
            allowInput = true;
        }
    }

    /// <summary>
    /// Commence la sélection de la case parmi les options données.
    /// </summary>
    /// <param name="options">Les options parmi lesquelles choisir</param>
    /// <remarks>Cette méthode attend un évènement <see cref="Case.OnTileChoiceStart"/>.</remarks>
    void StartTileSelection(Transform[] options)
    {
        //foreach(Transform option in options)
        //{
        //    Debug.Log($"pos {option.name}: {option.position}");
        //}

        allowMove = false;
        allowTileChoice = true;
    }

    /// <summary>
    /// Permet au joueur de choisir une case parmi les options disponibles.
    /// </summary>
    void ChooseTile()
    {
        foreach (KeyCode key in gameSettings.Players[GameManager.playerTurn].Controls.AllControls)
        {
            if (Input.GetKeyDown(key))
            {
                if (key == gameSettings.Players[GameManager.playerTurn].Controls.Action)
                {
                    // confirme le choix de case avec le bouton d'action
                    //Debug.Log(gameManager.tileChoice[0].gameObject.GetComponent<Case>().indexCase);
                    gameSettings.Players[GameManager.playerTurn].CurrentPos = gameManager.tileChoice[0].gameObject.GetComponent<Case>().indexCase - 1;
                    targetPos = gameManager.tileChoice[0].transform.position + playerPosAjust;
                    gameManager.popupTileChoice.SetActive(false);
                    foreach (GameObject obj in gameManager.tileChoiceiIndocators)
                    {
                        obj.SetActive(false);
                    }

                    OnTileChoiceEnd.Invoke();

                    allowTileChoice = false;
                    allowMove = true;
                }
                else
                {
                    // change la sélecton avec les autres boutons
                    Array.Reverse(gameManager.tileChoice);
                    for (int i = 0; i < gameManager.tileChoiceiIndocators.Length; i++)
                    {
                        gameManager.tileChoiceiIndocators[i].transform.position = gameManager.tileChoice[i].transform.position + new Vector3(0, 10);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Stock la position de redirection de la case.
    /// </summary>
    /// <param name="redirect">Le transform contenant la position de redirection</param>
    void TileRedirect(Transform redirect)
    {
        Debug.Log("planning redirect at: " + redirect);
        plannedRedirect = redirect.position + playerPosAjust;
        // -2 au lieu de -1 sinon ca saute une case... je sais pas pourquoi
        gameSettings.Players[GameManager.playerTurn].CurrentPos = redirect.gameObject.GetComponent<Case>().indexCase - 2;
    }
}
