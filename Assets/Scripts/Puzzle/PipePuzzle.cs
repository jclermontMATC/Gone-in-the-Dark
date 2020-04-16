// Sean Harvey

using UnityEngine;
using System.Collections.Generic;

public class PipePuzzle : MonoBehaviour {
    private PipePuzzlePiece[,] tiles; // Stores the tiles' positions within the grid
    private int startPosition = 11;
    private int endPosition = 5;
    private bool finished = false;

    /*
        Start and end positions are measured in tiles clockwise around the board starting at what would be the 11 hand on a clock.
        These numbers start at 0

           0 1 2
        11       3
        10       4
         9       5
           8 7 6
        
        Movement directions are defined as the direction the opening is facing clockwise from the top

          0
        3   1
          2
        
        The start and end positions are assigned movement directions accordingly
        These are calculated as follows: (floor(position number / 3) + 2) % 4
        Visual representation of the returned values shown below

           2 2 2
         1       3
         1       3
         1       3
           0 0 0
    */

    // Refreshes the tile positions
    // Used to keep track of positioning and which tiles are which
    public void RefreshTiles() {
        tiles = new PipePuzzlePiece[3,3];

        foreach (PipePuzzlePiece piece in transform.GetComponentsInChildren<PipePuzzlePiece>()) {
            RectTransform rectTransform = piece.GetComponent<RectTransform>();
            int tileScale = (int)rectTransform.rect.width;

            // Get the tile location using the position divided by the tile size.
            tiles[((int)rectTransform.localPosition.x + 180) / tileScale, -((int)rectTransform.localPosition.y - 180) / tileScale] = piece;
            piece.AssignBoard(this);
        }

        if (CheckForCompletion()) {
            finished = true;

            foreach (PipePuzzlePiece piece in transform.GetComponentsInChildren<PipePuzzlePiece>())
                piece.SetFinished(finished);
            
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
            // OPEN DOOR HERE!!!!!!!
        }
    }


    // Randomizes the positions of the tiles
    public void RandomizeTiles() {
        List<int> tiles = new List<int>(){ 0, 1, 2, 3, 4, 5, 6 };
        RectTransform rect = GetComponent<RectTransform>();

        for (int i=0; i<7; i++) {
            int index = (int)Random.Range(0, tiles.Count);
            RectTransform pieceRect = transform.Find(tiles[index].ToString()).GetComponent<RectTransform>();

            pieceRect.localPosition = new Vector2((i % 3) * pieceRect.rect.width, Mathf.FloorToInt((float)i / 3f) * -pieceRect.rect.height) - new Vector2(rect.rect.width / 2, -rect.rect.height / 2);

            tiles.RemoveAt(index);
        }
    }

    // Gets the movement direction of the starting position as defined in the main comment thread
    private int GetStartMovementDirection() {
        return (Mathf.FloorToInt(startPosition / 3) + 2) % 4;
    }

    // Gets the tile in a direction from a specific position
    private PipePuzzlePiece GetTileInDirection(Vector2 activeTile, int activeMovementDirection) {
        if (activeMovementDirection == 0) {
            activeTile.y--;
        } else if (activeMovementDirection == 1) {
            activeTile.x++;
        } else if (activeMovementDirection == 2) {
            activeTile.y++;
        } else if (activeMovementDirection == 3) {
            activeTile.x--;
        }
        
        if (activeTile.x < 0 || activeTile.x > 2 || activeTile.y < 0 || activeTile.y > 2) {
            return null;
        } else {
            return tiles[(int)activeTile.x, (int)activeTile.y];
        }
    }

    // Checks if a space is empty
    public bool IsSpaceEmpty(int x, int y) {
        if (x >= 0 & x <= 2 && y >= 0 && y <= 2) {
            return tiles[x, y] == null;
        } else { return false; }
    }

    // Moves a tile's data internally and returns true so the tile itself can physically move
    public bool MoveTile(Vector2 initialPosition, Vector2 newPosition) {
        if (Mathf.Abs(initialPosition.x - newPosition.x) + Mathf.Abs(initialPosition.y - newPosition.y) == 1) {
            if (IsSpaceEmpty((int)newPosition.x, (int)newPosition.y)) {
                tiles[(int)newPosition.x, (int)newPosition.y] = tiles[(int)initialPosition.x, (int)initialPosition.y];
                tiles[(int)initialPosition.x, (int)initialPosition.y] = null;

                return true;
            }
        }

        return false;
    }


    // Checks for completion
    // Follows a path from the start. If the path is interuppted at all the chain ends.
    // If the path leads to an edge that isn't the end the chain ends.
    // If the path leads directly from the start to the finish this method returns true.
    private bool CheckForCompletion() {
        for (int x=0; x<3; x++)
            for (int y=0; y<3; y++)
                if (tiles[x,y] != null)
                tiles[x,y].SetActiveColor(false);

        int activeMovementDirection = GetStartMovementDirection();
        Vector2 activeTile;
        PipePuzzlePiece tileObject;

        // Getting the intial tile.
        if (activeMovementDirection % 2 == 0) { // Even (Up and Down)
            if (activeMovementDirection == 0) {
                activeTile = new Vector2(2 - (startPosition % 3), 2);
            } else {
                activeTile = new Vector2(startPosition % 3, 0);
            }
        } else { // Odd (Left and Right)
            if (activeMovementDirection == 3) {
                activeTile = new Vector2(2, startPosition % 3);
            } else {
                activeTile = new Vector2(0, 2 - (startPosition % 3));
            }
        }

        tileObject = tiles[(int)activeTile.x, (int)activeTile.y];

        int iterations = 0;

        // Loops while chain is active.
        // Iterations is there in case something goes wrong and it starts infinitely looping.
        // If an infinite loop were to occur the iteration count would break the loop, prevent a crash.
        while (tileObject != null && iterations < 20) {
            activeMovementDirection = tileObject.GetNextDirection(activeMovementDirection);
            
            if (activeMovementDirection >= 0) {
                tileObject.SetActiveColor(true);
                tileObject = GetTileInDirection(activeTile, activeMovementDirection);

                // Move the position of the checked tile
                if (activeMovementDirection == 0) {
                    activeTile.y--;
                } else if (activeMovementDirection == 1) {
                    activeTile.x++;
                } else if (activeMovementDirection == 2) {
                    activeTile.y++;
                } else if (activeMovementDirection == 3) {
                    activeTile.x--;
                }

                // If the chain leads outside the regular grid
                if (activeTile.x < 0 || activeTile.x > 2 || activeTile.y < 0 || activeTile.y > 2) {
                    int exitIndex = -1;

                    if (activeTile.x < 0) {
                        exitIndex = 9 + (2 - ((int)activeTile.y % 3));
                    } else if (activeTile.x > 2) {
                        exitIndex = 3 + ((int)activeTile.y % 3);
                    } else if (activeTile.y < 0) {
                        exitIndex = (int)activeTile.x % 3;
                    } else if (activeTile.y > 2) {
                        exitIndex = 6 + (2 - (int)activeTile.x % 3);
                    }

                    // If the exit index is equal to the defined end position the method returns true as a chain has been completed.
                    return exitIndex == endPosition;
                }
            } else {
                return false;
            }
            
            iterations++;
        }

        return false;
    }

    private void Start() {
        RandomizeTiles();
        RefreshTiles();
    }
}
