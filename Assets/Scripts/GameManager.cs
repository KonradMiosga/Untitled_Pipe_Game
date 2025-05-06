using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private GameObject _manipulator;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private InOutPlacement _inOutPlacement;
    [SerializeField] public bool isGameWon = false;

    private PlayerMovement _playerMovement;
    private PipePlacement _pipePlacement;

    public GameObject _objectContainer;


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

    public void EndGame()
    {
        // GameObject.Find("MainMenu").SetActive(true);
        // GameObject.Find("Winning").SetActive(true);
        _menuManager.menu.SetActive(true);
        _menuManager.winMenu.SetActive(true);
        // winScreen.SetActive(true);
        DestroyBoundObjects();
    }

    void Update()
    {
        _manipulator.transform.position = _player.transform.position;
    }

    private void BindObjects()
    {
        // _mainDirectionalLight = Instantiate(_mainDirectionalLight);
        // _globalVolume = Instantiate(_globalVolume);
        // _gridManager = Instantiate(_gridManager);
        // _player = Instantiate(_player);
        // //_manipulator = Instantiate(_manipulator);
        // _inOutPlacement = Instantiate(_inOutPlacement);

        _objectContainer = new GameObject("SpawnedObjects");

        _mainDirectionalLight = Instantiate(_mainDirectionalLight, _objectContainer.transform);
        _globalVolume = Instantiate(_globalVolume, _objectContainer.transform);
        _gridManager = Instantiate(_gridManager, _objectContainer.transform);
        _player = Instantiate(_player, _objectContainer.transform);
        _inOutPlacement = Instantiate(_inOutPlacement, _objectContainer.transform);
    }

    private void DestroyBoundObjects()
    {
        if (_objectContainer != null)
            Destroy(_objectContainer);
    }


}
