using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _maxTimeSeconds;
    [SerializeField] private GameObject _startGameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text comtext;
    [SerializeField] private TMP_Text uncomtext;
    [SerializeField] private TMP_Text raretext;

    // Add member variables to store references to all of the UI text.

    private int _commonPoints;
    private int _uncommonPoints;
    private int _rarePoints;
    //public Vector2 mousePosition { get; };


    private float _timeRemaining;
    private bool _runGame;

    // ------------------------------------------------------------------------
    // You don't need to code anything in this method :)
    private void Start()
    {
        SetGameEnabled(false);
        UpdatePointsText();
    }



    // ------------------------------------------------------------------------
    private void Update()
    {
        if(_startGameUI.activeSelf==true){
            _player.transform.position = new Vector3(50, 0, 25);
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, 0);
            // Dampen towards the target rotation
            _player.transform.rotation = Quaternion.Slerp(transform.rotation, target,  1);
            //_player.transform.rotation = new Vector3(0,0,0);
        }
        if(_startGameUI.activeSelf==false && _gameOverUI.activeSelf==false){
            _runGame=true;
            if(_timeRemaining<=0){
                StartGame();
            }
        }
        else{
            _runGame=false;
        }
        
        //Debug.Log(_timeRemaining);
        if (_runGame)
        {
            _timeRemaining -= Time.deltaTime;
            
            _timeText.text = ("Time: " + _timeRemaining);

            if (_timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    // ------------------------------------------------------------------------
    public void StartGame()
    {
        _startGameUI.SetActive(false);
        _gameOverUI.SetActive(false);

        _timeRemaining = _maxTimeSeconds;

        SetGameEnabled(true);
        
        
        // Update the game timer text.
        _timeText.text = ("Time: " + _timeRemaining);
    }

    // ------------------------------------------------------------------------
    // You don't need to code anything in this method :)
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // ------------------------------------------------------------------------
    private void EndGame()
    {
        SetGameEnabled(false);
        _gameOverUI.SetActive(true);

        // Update the final score text in the game over UI.
        comtext.text = (_commonPoints+ " common");
        uncomtext.text = (_uncommonPoints+ " uncommon");
        raretext.text = (_rarePoints+ " rare");

        _timeText.text = ("Time: 0");

    }

    // ------------------------------------------------------------------------
    // You don't need to code anything in this method :)
    private void SetGameEnabled(bool enabled)
    {
        Cursor.lockState = enabled ? CursorLockMode.Locked : CursorLockMode.None;
        _player.enabled = enabled;
        _runGame = enabled;
    }

    // ------------------------------------------------------------------------
    // Add a paramter to this method to represent the item's rarity.
    public void CollectItem (string rarity)
    {
        // Using a SWITCH STATEMENT,
        // based on the item's rarity, update the appropriate points variable.
        switch (rarity){
            case "common":
            _commonPoints++;
            break;
            case "uncommon":
            _uncommonPoints++;
            break;
            case "rare":
            _rarePoints++;
            break;
        }

        UpdatePointsText();
    }

    // ------------------------------------------------------------------------
    public void RemoveItem (string rarity)
    {
        // Using a SWITCH STATEMENT,
        // based on the item's rarity, update the appropriate points variable.
        switch (rarity){
            case "common":
            _commonPoints--;
            break;
            case "uncommon":
            _uncommonPoints--;
            break;
            case "rare":
            _rarePoints--;
            break;
        }
        UpdatePointsText();
    }

    // ------------------------------------------------------------------------
    private void UpdatePointsText()
    {
        //_timeText.text = (_timeRemaining);
    }
}
    