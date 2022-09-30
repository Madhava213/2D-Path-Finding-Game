# 2D Path Finding Game

## Authors

-   Madhava Raveendra
-   Viet Nguyen

## Description

A 2D game with green circle nodes and brown square obstacles. The player can press 'R' or use the UI button in the game to reset the level (second 2 in video), press 'E' to spawn an obstacle at the origin. The user can also drag the obstacle around. The game will automatically regenerate the path which has no collision with any obstacles (second 3 in video). The player then can left click on any nodes for the start node and right click for the end node. The agent will move along the path from start to end (second 7 in video). If the player moves the obstacle to collide with the path, the game will automatically generate new path and the agent can follow the new path (second 11 in video).

## Features

-   Single Agent Navigation
-   Improved Agent & Scene Rendering
-   Orientation Smoothing
-   User Scenario Editing

## Code

We are using Unity3D Game Engine for the implementation of this project. We are using Unity's 2D Collider and Collision Detection Systems along with its Raycasting Libraries for the calculation of gameObject interaction. We are using a modified A Star Algorithm based off of the pseudocode we found in the references mentioned below. We are also using the 2D character - Astronaut provided by COPYSPRITE for free on the Unity Asset store as well as a publicly available background image. All other assets are custom made.

## Execution

Use the button below to navigate to the source code.
Find the 'Build' folder and run the executable inside.

## Difficulties

-   A\* implementation
-   Agent moving smoothly
-   Raycasting between obstacles, nodes, agent

## Video

[![Astronaut A Star] // Title
(url "/PublicAssets/Agent_Navigating.png")] // Thumbnail
(url "/PublicAssets/Video.mp4")

## Images

![Agent Navigation](url "/PublicAssets/Agent_Navigating.png")

## References

-   Wikipedia Pseudocode for A* Algorithm - https://en.wikipedia.org/wiki/A*\_search_algorithm
-   Astronaut Asset - https://assetstore.unity.com/packages/2d/characters/2d-character-astronaut-182650
