# Neat

Neat is a framework for in-code declarative Unity UI 💻 (using uGUI)

Currently totally WIP

## Basic concepts
There are three types of "building blocks" in Neat:

 - ### Widgets

    These are most similar to components in React. Their main function is to compose smaller blocks of interface and they contain "business" logic.

    It is widgets that can contain state or get it from your game's state.

    Mainly, you will write new Widgets and use Element and Components.

 - ### Element

    This is the only built-in representative of its kind. Basically, it represents a `GameObject` with a `RectTransform` in Unity scene hierarchy.

 - ### Components

    Components in Neat represent components attached to a `GameObject`. Unlike widgets, they don't have children and `Render` method, but have `Update` method.
    
    Components should be stateless in React meaning of it, just deriving Unity Component state from passed props.

    Components for all build-in uGUI components and TextMeshPro text are already included in Neat*,
    however, if you need to have some other Component be rendered through Neat, you will have to write a Component youself.

<sub>(*) Currently not all are implemented, WIP</sub>

