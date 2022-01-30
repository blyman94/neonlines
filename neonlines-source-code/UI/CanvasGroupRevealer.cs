using UnityEngine;

/// <summary>
/// Utility to show and hide CanvasGroups representing menus in the game's UI.
/// Works very cleanly with GameEventListeners.
/// </summary>
public class CanvasGroupRevealer : MonoBehaviour
{
    [Tooltip("CanvasGroup to be controlled by the revealer.")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Tooltip("Should this CanvasGroup start hidden?")]
    [SerializeField] private bool startHidden = false;

    [Tooltip("The alpha value this CanvasGroup will have when in the shown " +
        "state")]
    [SerializeField] private float shownAlpha = 1;

    [Tooltip("The BlockRaycast value this CanvasGroup will have when in the" +
        "shown state.")]
    [SerializeField] private bool shownBlockRaycast = true;

    [Tooltip("The Interactable value this Canvas group will have when in the " +
        "shown state.")]
    [SerializeField] private bool shownInteractable = true;

    /// <summary>
    /// Tracks whether the Canvas group is currently shown or hidden.
    /// </summary>
    private bool shown = true;

    private void Start()
    {
        if (startHidden)
        {
            HideGroup();
        }
    }

    /// <summary>
    /// Shows the CanvasGroup if its hidden, hides the canvas group if its 
    /// shown.
    /// </summary>
    public void Toggle()
    {
        if (shown)
        {
            HideGroup();
        }
        else
        {
            ShowGroup();
        }
    }

    /// <summary>
    /// Shows the CanvasGroup. Assigns shown state parameters Alpha,
    /// BlockRaycast and Interactable.
    /// </summary>
    public void ShowGroup()
    {
        shown = true;
        canvasGroup.alpha = shownAlpha;
        canvasGroup.blocksRaycasts = shownBlockRaycast;
        canvasGroup.interactable = shownInteractable;
    }

    /// <summary>
    /// Shows the CanvasGroup. Alpha is set to zero, BlockRaycasts is set to 
    /// false and Interactable is set to false.
    /// </summary>
    public void HideGroup()
    {
        shown = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
