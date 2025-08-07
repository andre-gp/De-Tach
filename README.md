# De-Tach
De-Tach is a toolkit that helps you decouple your game logic by using ScriptableObject-based variables, events, and utilities. Simplify communication between systems while keeping your Unity projects clean and modular.

This package is based on the ScriptableObject architecture proposed by Ryan Hipple. You can learn more about it in [this talk](https://www.youtube.com/watch?v=raQ3iHhE_Kk).

# Features

### Event-Based Approach
Use specialized event classes to broadcast changes across your game. Via their Inspector you can invoke events with custom values and, more importantly, view all objects currently listening to the event, with a convenient click-to-ping feature.

![Events](https://i.imgur.com/5ZbJ5wJ.png)

### Variables
Variables are a core feature of this package. Each variable holds a value and comes with built-in event support. You can define a default value for the variable, and you can access or modify the current value through the variable object. Whenever the value changes, an event is automatically broadcast to notify any listeners.

![Variable](https://i.imgur.com/agnnNbJ.png)

### Listener
The listener classes include a UnityEvent, allowing you to hook up responses in the Inspector and avoid hardcoding behaviors. When designing the game, you can easily swap variables or change responses without touching the code.

![Listener](https://i.imgur.com/N2fpFvu.png)

### Class Creation Wizard
Use the **Class Creator** to quickly generate variable and event classes for any type, making it easy to meet your specific needs and integrate them into your project.

![Class Creator](https://i.imgur.com/bHXxZTI.png)
