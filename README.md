# Conway's Game of Life

My implementation of Conway's Game of Life in C#.

* Any live cell with fewer than two live neighbours dies (referred to as underpopulation or exposure).
* Any live cell with more than three live neighbours dies (referred to as overpopulation or overcrowding).
* Any live cell with two or three live neighbours lives, unchanged, to the next generation.
* Any dead cell with exactly three live neighbours will come to life.

![ConwaysGameOfLife](https://github.com/awiswasi/ConwaysGameOfLife/blob/master/docs/assets/GameOfLife.gif)
