using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _playerPrefab;
    private GameObject _playerInstance;
    [SerializeField] private GridManager _gridManagerPrefab;
    private GridManager _gridManagerInstance;
    [SerializeField] private Light _mainDirectionalLight;
    private Light _mainDirectionalLightInstance;
    [SerializeField] private Volume _globalVolume;
    private Volume _globalVolumeInstance;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private InOutPlacement _inOutPlacementPrefab;
    private InOutPlacement _inOutPlacementInstance;
    [SerializeField] public bool isGameWon;

    private PlayerMovement _playerMovement;
    private PipePlacement _pipePlacement;

    public GameObject _objectContainer;


    public void StartGame()
    {
        isGameWon = false;
        BindObjects();

        _gridManagerInstance.InitializeGrid();
        _gridManagerInstance.DrawGridBounds();
        _gridManagerInstance.PlaceInCenter(_playerInstance);

        _playerMovement = _playerInstance.GetComponent<PlayerMovement>();

        _pipePlacement = _playerInstance.GetComponent<PipePlacement>();

        _inOutPlacementInstance.PlaceInOutInWorld();
    }

    public void EndGame()
    {
        _menuManager.menu.SetActive(true);
        _menuManager.winMenu.SetActive(true);
        DestroyBoundObjects();
    }

    public void LoseGame()
    {
        // Debug.Log("Spiel verloren!");
        _menuManager.menu.SetActive(true);
        _menuManager.looseMenu.SetActive(true);
        DestroyBoundObjects();
    }

    private void BindObjects()
    {
        _objectContainer = new GameObject("SpawnedObjects");

        _mainDirectionalLightInstance = Instantiate(_mainDirectionalLight, _objectContainer.transform);
        _globalVolumeInstance = Instantiate(_globalVolume, _objectContainer.transform);
        _gridManagerInstance = Instantiate(_gridManagerPrefab, _objectContainer.transform);
        _playerInstance = Instantiate(_playerPrefab, _objectContainer.transform);
        _inOutPlacementInstance = Instantiate(_inOutPlacementPrefab, _objectContainer.transform);
    }

    private void DestroyBoundObjects()
    {
        if (_objectContainer != null)
            Destroy(_objectContainer);
    }


}
