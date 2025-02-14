# 🚗 Driving Sim WPF

**Welcome to Driving Sim WPF!**  
This is a fun car simulation developed using WPF (Windows Presentation Foundation). The simulation allows you to interact with a car dashboard, controlling the engine, acceleration, and brakes, while the background dynamically adjusts to reflect the car's speed.

---

## 🎮 Game Description  
The goal of the simulation is to control a car, turning the engine on or off, accelerating, and braking. As you speed up, the background changes to simulate motion, and a pine air freshener image transforms into an animated GIF for extra fun! Enjoy the interactive, audiovisual experience!

---

## 🛠️ Main Methods  
Here are the key methods of the simulation:

### 1. Initialization  
- **`Car()`**: Initializes the car object with the car background and air freshener images. Sets up the timers for background and animation changes.  
- **`StartEngine()`**: Starts the engine, plays the engine startup sound, and begins the idle sound loop.  
- **`StopEngine()`**: Stops the engine, plays the engine off sound, and resets the speed.  

### 2. Car Controls  
- **`Accelerate()`**: Increases the speed of the car, adjusts the background speed, and updates the air freshener visibility.  
- **`Brake()`**: Slows down the car, decreases the speed, and updates the background speed.  

### 3. Visual Effects  
- **`ImageChangeTimer_Tick(object sender, EventArgs e)`**: Alternates the background images to simulate car movement.  
- **`UpdateAmbientador()`**: Changes the visibility of the air freshener image depending on the speed of the car.  
- **`StartGifAnimation()`**: Plays the animated GIF of the air freshener when the car reaches a certain speed.

---

## 🎮 How to Play  
1. **Start the engine**: Click the "Motor On" button to start the engine with sound effects.  
2. **Accelerate**: Press the "Accelerate" button to speed up the car and see the background change.  
3. **Brake**: Press the "Brake" button to slow down the car.  
4. **Turn off the engine**: Click the "Motor Off" button to stop the engine and reset the simulation.

---

## ⚙️ System Requirements  
- **OS**: Windows 7 or higher.  
- **.NET Framework**: Version 4.7.2 or higher.  
- **Resources**: Ensure resource files (images and sounds) are in the `assets` folder.  

---

## 📂 Project Structure  
- **`MainWindow.xaml`**: Contains the user interface elements for the car simulation, such as buttons and images.  
- **`MainWindow.xaml.cs`**: Contains the logic for interacting with the UI, such as handling button clicks to start/stop the engine, accelerate, and brake.  
- **`Car.cs`**: Contains the car control logic, including methods for starting/stopping the engine, accelerating, braking, and updating the visual effects.  
- **`assets/`**: Folder containing images and sounds used in the simulation.  


---

## 🎨 Credits  
- **Developed by**: Sergio Gracia Correia.  
- **Graphics and sound resources**: Assets created by me using Aseprite and Photodraw. Sounds extracted from [Pixabay](https://pixabay.com/sound-effects/).  
- **Inspiration**: This project started as a simple car simulation exercise to practice methods and object interaction, with the goal of creating a fun, interactive experience with WPF.

---

## 📜 License  
This project is licensed under the MIT License. Feel free to use, modify, and distribute it.  
