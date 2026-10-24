# GALib

A small genetic algorithm library in C#, written in 2019 as a project for a master's course in artificial intelligence. It is kept here as a record of that coursework, not as a maintained library.

## What It Does

`GALib` provides a generic `GeneticAlgorithm<Gene>` base class. A problem supplies its genotype, a random-member generator, a fitness function, and a way to draw an individual; the library runs the generational loop with optional elitism and duplicate suppression, and stops when a termination condition is met.

The operators are interchangeable:

| Stage | Methods |
|---|---|
| Selection | Fitness-proportionate, tournament, random |
| Crossover | Order 1 (OX1), partially mapped (PMX) |
| Mutation | Swap, reverse sequence, center inverse, none |
| Termination | Generation limit, solution found |

## Test App

`Test` is a Windows Forms testbed that runs two permutation problems and draws the best individual as it evolves:

- **N-Queens** - place N queens on an N x N board with no two attacking each other.
- **Traveling salesman** - find a short tour through a set of US airports, using latitude and longitude projected to Mercator coordinates, or through generated circle, donut, star, and random point sets.

The selection, crossover, and mutation methods are discovered by reflection and picked in the UI, so operators can be swapped and compared on the same problem.

## Building

Open `GALib.sln` in Visual Studio and build. Both projects target .NET Framework 4.8, and the test app runs on Windows only.
