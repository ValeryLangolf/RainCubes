using TMPro;
using UnityEngine;

public class ObjectCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalSpawnedText;
    [SerializeField] private TextMeshProUGUI _createdObjectsText;
    [SerializeField] private TextMeshProUGUI _objectsInSceneText;

    public void UpdateTotal(int value) =>
        UpdateText(_totalSpawnedText, value);

    public void UpdateCreatedObjects(int value) =>
        UpdateText(_createdObjectsText, value);

    public void UpdateInSceneObjects(int value) =>
        UpdateText(_objectsInSceneText, value);

    private void UpdateText(TextMeshProUGUI text, int value) =>
        text.text = value.ToString();
}