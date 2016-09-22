# RRT-Path-Planer-Visualization

I wrote this program as a an academic course project for Artificial Intelligence Course when I was at university.This is the 
implementation of Rapidly-Exploring Random Tree (RRT) path planning algorithm using C# and windows forms.

![Screen shot](https://github.com/k-timy/RRT-Path-Planer-Visualization/blob/master/screenShot.jpg)

In order to run this project follow the instructions below:

### Map File:
  An image file that consists of only two colors.White pixels are representative of walkable areas of a map.and Black pixels stand for obstacles.You can use the sample map file as an example.

### Introduce Map File to System
  You only need to set the "imageFilePath" private property in the Form1.cs file in any way you want.(I hard-coded for the ease
of implementation at the time of writing this application)

### Find the Path!
  Now,you've set that map variable you are now able to run the application.After running,you have 3 keys available.
Follow these steps:

1. Click on two different arbitrary points of the map.One by one.
**Note:** first click sets source point,and second click sets destination.
2. Now push the button labeld as "Draw Path".A dialog box appears to inform you of the time spent to find the path in milliseconds.
3. In case you want to clear the screen of the map and repeat the path finding steps,you may click on "Clear" button.

**Note 1:** The application does not guarantee to find an optimal path.Since it's focus is on finding a path in any way possible.
In the least count of steps.

**Note 2:** The application does not guarantee to find a path either! Since it is an inherent property of the RRT algorithm.

**Note 3:** The button labeled as "Nothing :D" actually does nothing :D

I would be glad to recieve your feedback on this project.

