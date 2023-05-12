# 2DUnityAStarPathfinding

This is an implementation of the A* Pathfinding algorithm in Unity using a grid-based system. The algorithm finds the shortest path between two points on the grid, while taking into account the walkability of the cells, as well as any additional weights assigned to the cells. Made over on https://www.twitch.tv/mirasonicrex with the help of chat! 

*** This algorithm is a work in progress and will be updated over time. Currently, there is no optimization for the node lists, which could lead to longer processing times and less efficient pathfinding. Future updates may include improvements to the data structures used and enhancements to the core algorithm, resulting in faster and more accurate pathfinding.


### Features
A* Pathfinding algorithm
Grid-based pathfinding
Supports different cell walkability levels
Diagonal movement (optional)
Debug visualization for path, start and end nodes


A* Pathfinding in Unity
This is an implementation of the A* Pathfinding algorithm in Unity using a grid-based system. The algorithm finds the shortest path between two points on the grid, while taking into account the walkability of the cells, as well as any additional weights assigned to the cells.

Features
A* Pathfinding algorithm
Grid-based pathfinding
Supports different cell walkability levels
Diagonal movement (optional)
Debug visualization for path, start and end nodes

### How to Use
Create a Grid object that represents the grid system for your game. The Grid object should have the following properties:
-Width
-Height
-CellSize
##### Initialize the AStarPathfinding class with the Grid object:
<pre><code>AStarPathfinding aStar = new AStarPathfinding(grid);</code></pre>
##### Call the FindPath method with the start and end positions to find the shortest path:

<pre><code>List<AStarPathfinding.Node> path = aStar.FindPath(startPos, endPos);</code></pre>
The FindPath method returns a list of Node objects that represent the path from the start to the end position.
Use path to move your game objects.


### Known Bugs: 
- Inconsistencies in tile selection: There may be instances where the selected tile does not match the expected tile, potentially due to rounding errors when converting between world and grid coordinates. This could result in slightly incorrect pathfinding results.
- NPCs don't always take the shortest path: In some cases, NPCs may not follow the optimal path to their destination. This could be caused by various factors, such as equal FCosts between two neighboring nodes, leading to suboptimal decisions.
