# Conway's Game of Life

My implementation of Conway's Game of Life in C# using test-driven development.

* Any live cell with fewer than two live neighbours dies (referred to as underpopulation or exposure).
* Any live cell with more than three live neighbours dies (referred to as overpopulation or overcrowding).
* Any live cell with two or three live neighbours lives, unchanged, to the next generation.
* Any dead cell with exactly three live neighbours will come to life.

## Example of Generations
![ConwaysGameOfLife](https://github.com/awiswasi/ConwaysGameOfLife/blob/master/docs/assets/GameOfLife.gif)

## Tests
![PassedTests](https://github.com/awiswasi/ConwaysGameOfLife/blob/master/docs/assets/PassedTests.PNG)
