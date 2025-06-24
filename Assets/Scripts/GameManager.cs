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
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;


    private PlayerMovement _playerMovement;
    private PipePlacement _pipePlacement;

    public GameObject _objectContainer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        _menuManager.menu.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // stoppt Spielzeit
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        _menuManager.menu.SetActive(false);
        Time.timeScale = 1f; // Spiel läuft weiter
        isPaused = false;
    }


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
        _menuManager.menu.SetActive(true);
        _menuManager.looseMenu.SetActive(true);
        DestroyBoundObjects();
    }

    public void BackToMenu()
    {
        _menuManager.menu.SetActive(true);
        pauseMenu.SetActive(false);
        _menuManager.mainMenu.SetActive(true);
        DestroyBoundObjects();
        Time.timeScale = 1f; // Spiel läuft weiter
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
