# Callsto

A simple call graph generator for assemblies built with .NET language.

## Usage

callsto [OPTION]... [FILE]...

OPTIONS
-c, --configuration     configuration file
-d, --debug
--include-external      include external assemblies, try to resolve and load
                        them
--include-system        include system BCL assemblies in the call graph
-o, --output            output stream (default: console), takes in file path
-f, --output-format     output format (default: interpret from filename)

[FILE] is valid .net assembly.

## Requirements
- Dependency between methods in current module
    - Process private methods

## Design

Building the graph
- Breadth first
    - Queue based navigation
    - Loop detection
- Graph representation
    ```
    Graph<T>: directed graph
        Node<T>: each node of the graph
            Edge: represents child edges, always directed out
                Properties<string, string>
                Node<T>

    - Leaf node has Edge count as zero
    ```
    - Properties will help us scale later with `arity` (1:N) relations or
      boundaries
- Operations on graph
    - Build/Insert
    - Read - required. Build various data export formats on top of this
    - Update - no op, data is static for us
    - Delete - required. User may choose to hide nodes
        - May not be required at runtime (or the presentation can handle
            directly)
- Statistics

### Composition
Data
- Paths -> Module -> produces a set of Types
- Type -> Method -> produces a set of Methods
- Method -> Explode -> Edges with Method and it's Child

```python
def explode(method, visited):
    if method in visited:
        return
    for child in method:
        yield (method, child)
        explode(child, visited + method)
```

Graph
- Edges -> Graph Engine -> Graph 

Graphs can be modelled in functional programming languages using Inductive
Graphs. See http://jelv.is/blog/Generating-Mazes-with-Inductive-Graphs/
https://stackoverflow.com/questions/30464163/functional-breadth-first-search

Output
- Graph -> Print -> Console
- Graph -> Export -> Dot file

API
- Module.With(path).With(path).Types    : IEnumerable<Type>
- Flatten<Method>(t => t.Methods)       : IEnumerable<Method>
- Explode                               : IEnumerable<Edge>
- ToForest                              : IEnumerable<Node>

## Data
- Represent in `dot` file format

## Presentation
### Console
- View: flattened tree similar to `pstree`
- Design
    - DFS walk through the graph

### Browser
- HTML
    - Use d3.js
    - https://github.com/mstefaniuk/graph-viz-d3-js

### Forms
- Continent
    - Nation of assemblies
        - States of classes
        - Cities of methods
    - Hover over a city, provides all links into the city
    - Like flight connectivity: https://bl.ocks.org/mbostock/7608400
