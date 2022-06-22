using NUnit.Framework;
using Rhino.Mocks;

namespace ConwaysGameOfLife.Tests
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void CellNumberOfNeighboursReturnsNeighbourCountFromGrid()
        {
            const int expectedNeighbourCount = 3;

            var grid = MockRepository.GenerateStub<Grid>(0, 0);
            var cell = new CellDouble(grid);
            grid.Stub(g => g.NumberOfNeighbours(cell))
                .Return(expectedNeighbourCount);

            Assert.That(cell.GetNumberOfNeighbours(), Is.EqualTo(expectedNeighbourCount));
        }

        [Test]
        public void DeadCellWithThreeLiveNeighboursBecomesLive()
        {
            var cell = AssumingCellWithNeighbours(State.Dead, 3);

            cell.BeginTick();

            Assert.That(cell.NextState, Is.EqualTo(State.Alive));
        }

        [Test]
        public void LiveCellWithFewerThanTwoLiveNeighboursDies()
        {
            var cell = AssumingCellWithNeighbours(State.Alive, 1);

            cell.BeginTick();

            Assert.That(cell.NextState, Is.EqualTo(State.Dead));
        }

        [Test]
        public void LiveCellWithMoreThanThreeLiveNeighboursDies()
        {
            var cell = AssumingCellWithNeighbours(State.Alive, 4);

            cell.BeginTick();

            Assert.That(cell.NextState, Is.EqualTo(State.Dead));
        }

        [Test]
        public void LiveCellWithTwoOrThreeLiveNeighboursLives()
        {
            var cellWithTwoNeighbours = AssumingCellWithNeighbours(State.Alive, 2);
            var cellWithThreeNeighbours = AssumingCellWithNeighbours(State.Alive, 3);

            cellWithTwoNeighbours.BeginTick();
            cellWithThreeNeighbours.BeginTick();

            Assert.That(cellWithTwoNeighbours.NextState, Is.EqualTo(State.Alive));
            Assert.That(cellWithThreeNeighbours.NextState, Is.EqualTo(State.Alive));
        }

        [Test]
        public void EndTickSetsCurrentStateToNextState()
        {
            const State expectedStateAfterTick = State.Dead;
            var cell = AssumingCellWithNeighbours(State.Alive, 0);
            cell.NextState = expectedStateAfterTick;

            cell.EndTick();

            Assert.That(cell.CurrentState, Is.EqualTo(expectedStateAfterTick));
        }

        private Cell AssumingCellWithNeighbours(State state, int numberOfNeighbours)
        {
            var grid = MockRepository.GenerateStub<Grid>(0, 0);
            var cell = new Cell(grid, 0, 0) { CurrentState = state };
            grid.Stub(g => g.NumberOfNeighbours(cell))
                .Return(numberOfNeighbours);
            return cell;
        }

        private class CellDouble : Cell
        {
            public CellDouble(Grid grid)
                : base(grid, 0, 0)
            {
            }

            public new int GetNumberOfNeighbours()
            {
                return base.GetNumberOfNeighbours();
            }
        }
    }
}