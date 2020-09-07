# Bikey McBikeface
This is the final project for EECS 4471: Introduction to Virtual Reality. The team of 3 undergraduate students -- Diana Lee, Andrew Colvin and Rui Amoah -- decided to explore the locomotion hardware in virtual reality and how the visual/haptic feedback could substitute the limitations of the hardware equipment.

## Table of Contents
1. [Youtube Summary](#youtube-summary)
2. [Description](#project-description)
3. [Core Functionalities - How to use the system](#core-functionalities)
4. [Dependencies](#dependencies)
5. [Credits](#credits)

## YouTube Summary
[![Screenshot Image of Overlay Screen Menu](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/youtubeThumbnail.png)](https://www.youtube.com/watch?v=MPE8MiIXm6M)

## Project Description
Bikey McBikeface is a simple interactive pedaling system for navigation applications in virtual environment. It features intuitive interfaces involving the pedaling pace, steering gesture and realistic feedback. The following diagram below summarizes the overall design and architecture of Bikey McBikeface project which consists of two major components: stationary pedal exerciser with a 3D printed gear system and a rotary encoder connected to the Arduino Nano, and a pair of Oculus Touch controllers. The project team also developed two sceneries, cityscape and off-the-road environment, to emulate virtual bike-riding experiences.
![Summary Diagram](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/diagram.png)

### Stationary Pedal exerciser
For this project, the team utilized a small off-the-shelf under desk pedals. A user continuously interacts with the exerciser by simply pedalling; it detects the user's speed via a combination of a rotary encoder and 3D printed gear system. The rotary encoder acting as the rotation sensor monitors the angular positions of the pedals in combination with the mounted 3D printed gears; these monitored values are then fed into an attached Arduino Nano, which is connected back to the computer over USB. The Arduino component contains a simple software script that calculates the revolutions per minute (RPM) of the bike pedals and transmits it to the computer via serial communications. On the computer side, a plugin called Ardity is used to extract all the collected and calculated data through the serial port, enabling the Unity game objects to access the data.
![Exerciser in details](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/pedals_details.png)

### Oculus Touch Controllers
In order to monitor the hand movements of a user, a pair of Oculus Touch Controllers is used. In order to enable the steering functionality for the bike, the team adopted the same steering mechanism as that of a car. Similar to how a car driver moves the steering wheel in an up and down motion by tilting their hands in reality, users can steer their virtual bike in the same way. However, unlike the actual car steering wheel, the controllers are not physically connected to each other. This detachment provides the player more flexibility when it comes to bike steering; for example, instead of constantly holding their hands up in the air, they can let them rest beside their bodies and simply tilt the bodies as well.
![Touch Controllers and their representation in Unity](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/turn_left.png)

### Virtual Bike Representation in Unity
In Unity, we created two different tracks where the players can explore by riding their virtual bikes. One of them is a rural, off-road track of a highway going through a mountain scape. The track mainly consists of uphill roads, allowing the players to fully immerse themselves in the effects of going uphill and downhill. The other track represents a cityscape scene, where the roads are mostly flat but with obstacles such as speed bumps.
To model and simulate the physics of a real bike wheel, we developed the virtual bike fully utilizing the functionalities provided by the combination of the Stationary Pedal Exerciser and Touch Controllers. Additionally, the virtual bike is constructed based on Unity's built-in Wheel Collider functionality, which allows the project to take advantage of slip-based friction to model the bike tires going through different terrain types.
![Two Tracks in Unity and bike design](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/scene_horizontal.png)

## Core Functionalities
The best way to start the Unity project is via Unity's build and run function after connecting the pedals to the computer. However, the project allows two different play modes: one with pedals and the other without bike pedals. In both cases, the pair of Oculus Touch Controllers would be the main interaction tool. Using the two controllers, a user would have full access to all functionalities supported by the project.

### With Bike Pedals
The following diagram summarizes the core functionalities supported with bike pedals:
![Functionalities supported with bike pedals](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/with_pedals.png)

### Without Bike Pedals
The following diagram summarizes the core functionalities supported without bike pedals:
![Functionalities supported without bike pedals](https://github.com/dianalee1022/Bikey-McBikeface/blob/master/Images/without_pedals.png)

## Dependencies
A brief description of each class/script created and any dependencies (on other sources/resources) used in the project.
Majority of the scripts/class using Unity vehicles tools. The GameObject called carRoot is the root of the generated bike, and has the following components: an EasySuspension script, a WheelDrive script and a Rigidbody. The EasySuspension script is a utility script that updates suspension settings across all child WheelCollider components. Simply enter the mass, natural frequency and damping ratio for the wheels, and the EasySuspension script will take care of updating each WheelCollider component with these settings. The WheelDrive component is a minimalistic bike controller. This script allows for quick testing out of the box and provides a good start for your own customized vehicle controller.
We edit WheelCollider to introduce the input from Oculus Rift using the OVR library and Ardity library to connect the bike pedal from Arduino Uno to Unity. Sound script was added for the bike’s bell and brakes and the correct haptic plays with the sound. Also, when pedaling and when you are moving but not pedaling different sounds play for the correct movement. Respawn point are added to both scene and scene switching are both implemented and controlled using the Oculus Rift. Lastly, haptic script was added to the speed bumps.

## Credits
* The Cityscape used were purchased from [Polyfix](https://assetstore.unity.com/packages/3d/environments/urban/detailed-city-construction-pack-v-2-49183) which is available on the Unity Asset Store. This pack is only available for EECS4471 final project purpose and should not be used outside of the course.
* The rural, off-road scene was adopted from one of Unity's tutorial.
