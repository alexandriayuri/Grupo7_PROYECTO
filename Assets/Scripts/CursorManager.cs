using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture; // Custom cursor texture
    public AudioClip hoverSound;   // Audio clip for hover sound

    private AudioSource audioSource; // AudioSource component
    private VisualElement rootVisualElement;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set the custom cursor
        SetCursor(cursorTexture, Vector2.zero, 0.5f);

        // Get all buttons in the UI
        var document = GetComponent<UIDocument>();
        rootVisualElement = document.rootVisualElement;

        var buttons = rootVisualElement.Query<Button>().ToList();

        // Register event listeners for each button
        foreach (var button in buttons)
        {
            button.RegisterCallback<MouseEnterEvent>(evt => OnButtonHover());
        }
    }

    private void SetCursor(Texture2D cursorTexture, Vector2 hotspot, float scaleFactor)
    {
        // Calculate the scaled width and height
        int scaledWidth = Mathf.RoundToInt(cursorTexture.width * scaleFactor);
        int scaledHeight = Mathf.RoundToInt(cursorTexture.height * scaleFactor);

        // Calculate the scaled hotspot
        Vector2 scaledHotspot = new Vector2(hotspot.x * scaleFactor, hotspot.y * scaleFactor);


        UnityEngine.Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    private void PlayHoverSound()
    {
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    private void OnButtonHover()
    {
        PlayHoverSound(); // Play hover sound
    }

    private void OnLabelHover()
    {
        PlayHoverSound(); // Play hover sound
    }
}
