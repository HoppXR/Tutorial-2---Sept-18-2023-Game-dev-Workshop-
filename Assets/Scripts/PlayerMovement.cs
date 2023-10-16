using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    //inspector value
    [SerializeField] private List<Player> _players;

    public float _speed;

    [SerializeField] private Board _board = new Board();

    [SerializeField] private Text _text;

    Vector2 _currentPos;
    Vector2 _nextPos;

    float _totalTime;
    float _deltaT;

    bool _isMoving;
    bool _gameIsOver;
    bool _turnStarted;

    int _currentPlayer = 0;
    int _tileMovementAmount;

    Die _die = new Die();
    // Start is called before the first frame update
    void Start()
    {
        _board.InitTilePositions();
        _text.text = "Press 'Space to roll the dice";
    }

    void CheckWin()
    {
        if (_gameIsOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("BoardGame");
        }

        if ((_players[0].GetCurrentTile() == 100 || _players[1].GetCurrentTile() == 100) && !_isMoving)
        {
            _gameIsOver = true;
            _text.text = "Game Over! Player "+(_currentPlayer + 1 )+" Wins! Press 'Space' to play again";
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();

        if (!_gameIsOver)
        {
            _currentPos = _players[_currentPlayer].GetPosition();

            if (_currentPlayer == 0)
            {
                _text.color = Color.cyan;
            }
            else
            {
                _text.color = Color.yellow;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _tileMovementAmount == 0)
            {
                _tileMovementAmount = _die.RollDice();
                _text.text = "You rolled a " + _tileMovementAmount.ToString();
                _turnStarted = true;
            }

            if (_tileMovementAmount > 0 && !_isMoving)
            {
                MoveOneTile();
            }

            if (_isMoving)
            {
                UpdatePosition();
            }

            if (_tileMovementAmount == 0 && !_isMoving)
            {
                _text.text = "Press 'space' to roll the dice";

                if (_turnStarted)
                {
                    _turnStarted = false;

                    if (_currentPlayer == 0)
                    {
                        _currentPlayer = 1;
                    }
                    else
                    {
                        _currentPlayer = 0;
                    }
                }
            }
        }
    }

    void UpdatePosition()
    {
        _deltaT += Time.deltaTime / _totalTime;

        if (_deltaT < 0f)
        {
            _deltaT = 0f;
        }

        if (_deltaT >= 1f || _nextPos == _currentPos)
        {
            _isMoving = false;
            _tileMovementAmount--;
            _deltaT = 0f;
        }
        _players[_currentPlayer].SetPosition(Vector2.Lerp(_currentPos, _nextPos, _deltaT));
    }

    void MoveOneTile()
    {
        _nextPos = _board.GetTilePositions()[_players[_currentPlayer].GetCurrentTile()];
        _totalTime = (_nextPos - _currentPos / _speed).magnitude;
        _isMoving = true;
        _players[_currentPlayer].SetCurrentTile(_players[_currentPlayer].GetCurrentTile() + 1);
    }
}