using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject directionPanel; // Panel that contains direction buttons

    [SerializeField]
    private Button directionButtonPrefab; // Prefab for a direction button

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Show direction choices at a crossroad
    public void ShowDirectionChoices(List<Direction> availableDirections, Action<Direction> onDirectionChosen)
    {
        // Activate the panel
        directionPanel.SetActive(true);

        // Clear any existing buttons
        foreach (Transform child in directionPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each available direction
        foreach (Direction direction in availableDirections)
        {
            Button newButton = Instantiate(directionButtonPrefab, directionPanel.transform);
            newButton.GetComponentInChildren<Text>().text = direction.ToString(); // Set button label to direction name

            // Add click listener to notify when this direction is chosen
            newButton.onClick.AddListener(() =>
            {
                directionPanel.SetActive(false); // Hide the panel
                onDirectionChosen(direction); // Notify the PlayerMovement script
            });
        }
    }
}
