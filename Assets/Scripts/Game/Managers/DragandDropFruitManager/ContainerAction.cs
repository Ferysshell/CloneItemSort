using UnityEngine;

public class ContainerAction : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField] private GameObject _selectedContainer;
    private Container _selectContainer;
    [SerializeField] private Transform _selectedFruit;
    [Header("Points Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _pointForMove;
    [Header("Other Settings")]
    [SerializeField] private bool _isSelectedContainer;
    private DragAndDropFruit _dragAndDropFruit;

    private void Start()
    {
        _isSelectedContainer = false;
        _dragAndDropFruit = GetComponent<DragAndDropFruit>();
    }

    void Update()
    {
        if (!_dragAndDropFruit.fruitMove && Input.touchCount > 0 && !_dragAndDropFruit.fruitMove)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleTouch(touch.position);
                SetSelectedStatusForContainer();
            }
        }
    }

    void HandleTouch(Vector3 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject touchedObject = hit.collider.gameObject;
            if (touchedObject.CompareTag("Container"))
            {
                _selectContainer = touchedObject.GetComponent<Container>();
                if (!_selectContainer.containerIsClose) CheckSelectedContainer(touchedObject);
            }
        }
    }

    private void CheckSelectedContainer(GameObject touchedObject)
    {
        if (!_isSelectedContainer && _selectedContainer == null && _selectedFruit == null)
        {
            _selectedContainer = touchedObject;
            SelectContainer(touchedObject);
        }
        else
        {
            if (touchedObject && _selectedContainer)
            {
                Container selectedContainer = _selectedContainer.GetComponent<Container>();
                if (selectedContainer.id != _selectContainer.id && !_selectContainer.IsConteinerFull())
                {
                    _dragAndDropFruit.SetFruit(_selectedFruit, touchedObject, _selectedContainer.transform.GetChild(2));
                    _selectedContainer = null;
                    _selectedFruit = null;
                    _startPoint = null;
                    _pointForMove = null;
                }
                else
                {
                    DeselectContainer();
                }
            }
        }
    }

    private void SetSelectedStatusForContainer()
    {
        _isSelectedContainer = !_isSelectedContainer;
    }

    private void SelectContainer(GameObject Container)
    {
        _pointForMove = Container.transform.GetChild(2);
        Transform fruitContainer = Container.transform.GetChild(0);

        for (int i = fruitContainer.childCount - 1; i >= 0; i--)
        {
            Transform point = fruitContainer.GetChild(i);
            if (point.childCount > 0)
            {

                _startPoint = point;
                _selectedFruit = point.GetChild(0);
                StartCoroutine(_dragAndDropFruit.MoveVerticalFruitToPoint(_selectedFruit, _pointForMove.position));
                return;
            }
        }
    }

    private void DeselectContainer()
    {
        if (_selectedFruit != null && _startPoint != null)
        {
            StartCoroutine(_dragAndDropFruit.MoveVerticalFruitToPoint(_selectedFruit, _startPoint.position));
        }
        _selectedContainer = null;
        _selectedFruit = null;
        _startPoint = null;
        _pointForMove = null;
    }
}