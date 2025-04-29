using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private GameObject _manipulator;
    [SerializeField] private InOutPlacement _inOutPlacement;

    private PlayerMovement _playerMovement;
    private PipePlacement _pipePlacement;

    public List<(int, int, int)> sizes = new List<(int, int, int)>();

    public void SetGridSize(List<(int, int, int)> sizes)
    {
        //_gridManager.gridSize = gridSize;
    }
    public void StartGame()
    {

        BindObjects();

        _gridManager.InitializeGrid();
        _gridManager.DrawGridBounds();
        _gridManager.PlaceInCenter(_player);

        _playerMovement = _player.GetComponent<PlayerMovement>();

        _pipePlacement = _player.GetComponent<PipePlacement>();

        _inOutPlacement.PlaceInOutInWorld();


    }

    void Update()
    {
        _manipulator.transform.position = _player.transform.position;
    }

    private void BindObjects()
    {
        _mainDirectionalLight = Instantiate(_mainDirectionalLight);
        _globalVolume = Instantiate(_globalVolume);
        _gridManager = Instantiate(_gridManager);
        _player = Instantiate(_player);
        //_manipulator = Instantiate(_manipulator);
        _inOutPlacement = Instantiate(_inOutPlacement);
    }

}
