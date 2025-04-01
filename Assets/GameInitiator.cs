using UnityEngine;
using UnityEngine.Rendering;

public class GameInitiator : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private Volume _globalVolume;
    //[SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _manipulator;
    private void Start()
    {

        BindObjects();

        _gridManager.InitializeGrid();
        _gridManager.DrawGridBounds();

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
        //_mainCamera = Instantiate(_mainCamera);
        //_mainCamera.transform.SetParent(_player.transform, true);
        _manipulator = Instantiate(_manipulator);
    }

}
