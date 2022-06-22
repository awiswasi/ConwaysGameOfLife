using NUnit.Framework;
using Rhino.Mocks;

namespace ConwaysGameOfLife.Tests
{
    [TestFixture]
    public class GridTest
    {
        [Test]
        public void InitialiseCreatesGridPopulatedWithCells()
        {
            const int expectedWidth = 10;
            const int expectedHeight = 20;
            var grid = new GridDouble(expectedWidth, expectedHeight);

            grid.Initialise();

            foreach (var column in grid.Cells)
            {
                foreach (var cell in column)
                {
                    Assert.That(cell, Is.InstanceOf(typeof(Cell)));
                }
            }
        }

        [TestCase(10, 20, State.Default)]
        [TestCase(50, 75, State.Alive)]
        [TestCase(120, 30, State.Dead)]
        public void CellAtReturnsCellWithCorrectData(int x, int y, State state)
        {
            var grid = GetTestGrid();
            var cell = grid.CellAt(x, y);

            Assert.That(cell.X, Is.EqualTo(x));
            Assert.That(cell.Y, Is.EqualTo(y));
            Assert.That(cell.CurrentState, Is.EqualTo(state));
        }

        [TestCase(30, 20, ExpectedResult = 1)]
        [TestCase(31, 20, ExpectedResult = 2)]
        [TestCase(32, 20, ExpectedResult = 1)]
        [TestCase(50, 75, ExpectedResult = 2)]
        [TestCase(50, 76, ExpectedResult = 3)]
        [TestCase(50, 77, ExpectedResult = 2)]
        [TestCase(51, 76, ExpectedResult = 3)]
        [TestCase(149, 0, ExpectedResult = 0)]
        [TestCase(75, 99, ExpectedResult = 1)]
        public int NumberOfNeighboursFor(int x, int y)
        {
            var grid = GetTestGrid();
            var cell = new Cell(grid, x, y);

            return grid.NumberOfNeighbours(cell);
        }

        [Test]
        public void TickCallsBeginTickFollowedByEndTickOnAllCells()
        {
            var grid = new GridDouble(2, 2);
            var cell = MockRepository.GenerateMock<Cell>(grid, 0, 0);

            grid.Initialise();
            grid.Cells[0][0] = cell;
            grid.Cells[1][0] = cell;
            grid.Cells[0][1] = cell;
            grid.Cells[1][1] = cell;

            grid.Tick();

            cell.AssertWasCalled(c => c.BeginTick(), o => o.Repeat.Times(4));
            cell.AssertWasCalled(c => c.EndTick(), o => o.Repeat.Times(4));
        }

        private Grid GetTestGrid()
        {
            var grid = new GridDouble(150, 100);
            grid.Initialise();

            // Column of three live cells
            grid.Cells[30][20].CurrentState = State.Alive;
            grid.Cells[31][20].CurrentState = State.Alive;
            grid.Cells[32][20].CurrentState = State.Alive;

            // Cells in 'T' shape
            grid.Cells[50][75].CurrentState = State.Alive;
            grid.Cells[50][76].CurrentState = State.Alive;
            grid.Cells[50][77].CurrentState = State.Alive;
            grid.Cells[51][76].CurrentState = State.Alive;

            // Cell in top right
            grid.Cells[149][0].CurrentState = State.Alive;

            // Cells on bottom middle
            grid.Cells[74][99].CurrentState = State.Alive;
            grid.Cells[75][99].CurrentState = State.Alive;

            // Explicitly dead cell
            grid.Cells[120][30].CurrentState = State.Dead;

            return grid;
        }

        private class GridDouble : Grid
        {
            public GridDouble(int width, int height)
                : base(width, height)
            {
            }

            public Cell[][] Cells
            {
                get { return cells; }
            }
        }
    }
}