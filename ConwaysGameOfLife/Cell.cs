namespace ConwaysGameOfLife
{
    public class Cell
    {
        private readonly Grid grid;

        public Cell(Grid grid, int x, int y)
        {
            this.grid = grid;
            X = x;
            Y = y;
        }

        public State CurrentState { get; set; }
        public State NextState { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected int GetNumberOfNeighbours()
        {
            return grid.NumberOfNeighbours(this);
        }

        public virtual void BeginTick()
        {
            int numberOfNeighbours = GetNumberOfNeighbours();

            if (CurrentState == State.Alive)
            {
                if (numberOfNeighbours < 2) NextState = State.Dead;
                if (numberOfNeighbours >= 2 && numberOfNeighbours <= 3) NextState = State.Alive;
                if (numberOfNeighbours > 3) NextState = State.Dead;
            }
            else if (numberOfNeighbours == 3)
            {
                NextState = State.Alive;
            }
        }

        public virtual void EndTick()
        {
            CurrentState = NextState;
            NextState = State.Default;
        }
    }

    public enum State
    {
        Default,
        Alive,
        Dead
    }
}