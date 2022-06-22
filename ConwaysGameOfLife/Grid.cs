using System;


namespace ConwaysGameOfLife
{

    public class Grid
    {
        private readonly int width;
        private readonly int height;
        protected readonly Cell[][] cells;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new Cell[width][];
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public void Initialise()
        {
            for (var widthCount = 0; widthCount < width; widthCount++)
            {
                cells[widthCount] = new Cell[height];

                for (var heightCount = 0; heightCount < height; heightCount++)
                {
                    cells[widthCount][heightCount] = new Cell(this, widthCount, heightCount);
                }
            }
        }

        public void Tick()
        {
            ActOnCells(c => c.BeginTick());
            ActOnCells(c => c.EndTick());
        }

        private void ActOnCells(Action<Cell> cellAction)
        {
            for (var widthCount = 0; widthCount < width; widthCount++)
            {
                for (var heightCount = 0; heightCount < height; heightCount++)
                {
                    cellAction(cells[widthCount][heightCount]);
                }
            }
        }

        public virtual int NumberOfNeighbours(Cell cell)
        {
            var x = cell.X;
            var y = cell.Y;
            var liveNeighbourCells = 0;

            if (CellIsAlive(x - 1, y - 1)) liveNeighbourCells++; // top left
            if (CellIsAlive(x, y - 1)) liveNeighbourCells++; // top middle
            if (CellIsAlive(x + 1, y - 1)) liveNeighbourCells++; // top right

            if (CellIsAlive(x - 1, y)) liveNeighbourCells++; // middle left
            if (CellIsAlive(x + 1, y)) liveNeighbourCells++; // middle right

            if (CellIsAlive(x - 1, y + 1)) liveNeighbourCells++; // bottom right
            if (CellIsAlive(x, y + 1)) liveNeighbourCells++; // bottom middle
            if (CellIsAlive(x + 1, y + 1)) liveNeighbourCells++; // bottom left

            return liveNeighbourCells;
        }

        private bool CellIsAlive(int x, int y)
        {
            if (x < 0 || y < 0 || x > width - 1 || y > height - 1) return false;

            return CellAt(x, y).CurrentState == State.Alive;
        }

        public Cell CellAt(int x, int y)
        {
            return cells[x][y];
        }
    }
}