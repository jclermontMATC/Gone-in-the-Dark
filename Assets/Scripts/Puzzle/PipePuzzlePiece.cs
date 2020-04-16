using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipePuzzlePiece : MonoBehaviour
     , IPointerUpHandler
     , IDragHandler {
    private bool tracking = false;
    private RectTransform rect;
    private RectTransform boardRect;
    private bool finished = false;

    private PipePuzzle board;
    [SerializeField] private int direction0;
    [SerializeField] private int direction1;
    private Vector2 boardOffset;
    private Vector2 currentPosition;

    // While the puzzle isn't finished and the player is holding the tile, it constantly checks if it can move.
    private void Update() {
        if (!finished && tracking) {
            Vector2 mousePosition = (Vector2)Input.mousePosition;
            mousePosition.y = -(Screen.height - mousePosition.y);
            mousePosition -= boardOffset;
            
            if (mousePosition.x >= 0 && mousePosition.x <= boardRect.rect.width && mousePosition.y <= 0 && mousePosition.y >= -boardRect.rect.height) {
                Vector2 newPosition = new Vector2(Mathf.Floor(mousePosition.x / rect.rect.width), Mathf.Floor(-mousePosition.y / rect.rect.height));
                Vector2 offset = newPosition - currentPosition;

                if (offset.x != 0)
                    offset.x = offset.x / Mathf.Abs(offset.x);
                
                if (offset.y != 0)
                    offset.y = offset.y / Mathf.Abs(offset.y);
                
                if (offset.x != 0 || offset.y != 0) {
                    if (board.IsSpaceEmpty((int)currentPosition.x + (int)offset.x, (int)currentPosition.y)) {
                        if (board.MoveTile(currentPosition, currentPosition + (offset * new Vector2(1, 0)))) {
                            rect.localPosition += (Vector3)(offset * new Vector2(1, 0) * rect.rect.width);
                            UpdateCurrentPosition();
                            board.RefreshTiles();
                            return;
                        }
                    }

                    if (board.IsSpaceEmpty((int)currentPosition.x, (int)currentPosition.y + (int)offset.y)) {
                        if (board.MoveTile(currentPosition, currentPosition + (offset * new Vector2(0, 1)))) {
                            rect.localPosition += (Vector3)(offset * new Vector2(0, -1) * rect.rect.height);
                            UpdateCurrentPosition();
                            board.RefreshTiles();
                            return;
                        }
                    }
                }
            }
        }
    }

    // Grabbing events
    public void OnDrag(PointerEventData eventData) {
        tracking = eventData.dragging;
    }

    public void OnPointerUp(PointerEventData eventData) {
        tracking = false;
    }

    // Sets the current position as grid placement for more performant checks in the Update loop.
    private void UpdateCurrentPosition() {
        currentPosition = new Vector2(Mathf.Floor((rect.localPosition.x + (boardRect.rect.width / 2)) / rect.rect.width), Mathf.Floor(-(rect.localPosition.y - (boardRect.rect.height / 2)) / rect.rect.height));
    }

    // Get the next movement direction based on the input.
    // Returns -1 if the given initalDirection don't match either side of the tile.
    public int GetNextDirection(int initialDirection) {
        int matchingDirection = (initialDirection + 2) % 4;

        if (direction0 == matchingDirection) {
            return direction1;
        } else if (direction1 == matchingDirection) {
            return direction0;
        } else {
            return -1;
        }
    }


    // Sets the color of the tile based on whether it is currently connected to the chain or not.
    public void SetActiveColor(bool active) {
        if (active) {
            GetComponent<Image>().color = new Color(0.1f, 0.625f, 1f);
        } else {
            GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    // Assigns the board to the tile, allows for checking of other tiles' positions
    public void AssignBoard(PipePuzzle _board) {
        rect = GetComponent<RectTransform>();

        board = _board;
        boardRect = board.GetComponent<RectTransform>();
        boardOffset = new Vector2(Screen.width / 2, -Screen.height / 2) - new Vector2(180, -180);

        UpdateCurrentPosition();
    }

    // Self explanitory
    public void SetFinished(bool _finished) {
        finished = _finished;
    }
}
