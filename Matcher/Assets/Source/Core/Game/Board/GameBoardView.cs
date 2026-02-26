using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Core.Game.Board
{
    public class GameBoardView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private RectTransform _boardRect;

        public void AdjustGridLayout(int totalElements, int columns)
        {
            _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayout.constraintCount = columns;

            int rows = Mathf.CeilToInt((float)totalElements / columns);

            float availableWidth = _boardRect.rect.width - _gridLayout.padding.left - _gridLayout.padding.right - (_gridLayout.spacing.x * (columns - 1));
            float availableHeight = _boardRect.rect.height - _gridLayout.padding.top - _gridLayout.padding.bottom - (_gridLayout.spacing.y * (rows - 1));

            float cellWidth = availableWidth / columns;
            float cellHeight = availableHeight / rows;
            
            float finalCellSize = Mathf.Min(cellWidth, cellHeight);
            
            _gridLayout.cellSize = new Vector2(finalCellSize, finalCellSize);
        }
    }
}