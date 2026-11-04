# Digital Immersion as an Educational Tool
### Interactive Math Simulations in a Game Environment (Unity & C#)

![Unity](https://img.shields.io/badge/Unity-2D%2F3D-blue?logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-green?logo=csharp)
![Architecture](https://img.shields.io/badge/Architecture-Data--Driven-orange)

An interactive 2D educational game proof-of-concept developed in **Unity** using **C#**. This project bridges game-based learning (GBL) with practical mathematical problem-solving, turning abstract formulas into immersive gameplay mechanics.

---

## 📌 Project Overview

Traditional math education often suffers from abstract presentation and formula memorization. This project addresses that challenge by integrating historical mathematical concepts—such as **Thales' Shadow Measurement Method**—directly into game progression.

Players explore an interactive world where mathematical challenges act as keys to unlocking obstacles and furthering the story narrative.

---

## 🏗️ Technical Architecture & Key Features

* **Data-Driven Design (ScriptableObjects):** 
  * Implemented Unity `ScriptableObjects` (`FormulaData`) to separate mathematical puzzle data (questions, variables, solutions) from underlying engine logic.
  * Allows educators or designers to add or modify educational content directly within the Unity Editor without recompiling source code.
* **Core Gameplay Loop:**
  1. **Challenge Discovery:** Environment-based spatial puzzle detection.
  2. **Interactive Math Interface:** Real-time formula manipulation using world-collected data.
  3. **Instant Evaluation & Feedback:** Non-punitive iterative learning through immediate feedback.
  4. **State Progression:** Dynamic world state updates upon puzzle resolution.
* **Version Control:** Managed using Git with strict Unity `.gitignore` policies.

---

## 🛠️ Tech Stack & Tools

* **Engine:** Unity Engine (C# / .NET)
* **Architecture:** Data-Driven Architecture (`ScriptableObjects`)
* **Design Methodology:** Game-Based Learning (GBL) & Immersion Theory (Gee, 2003; Prensky, 2001)

---

## 🚀 How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/YOUR-USERNAME/2d-math-game.git
   ```
2. Open **Unity Hub**.
3. Select **Add Project from Disk** and choose the cloned folder.
4. Open the main scene located in `Assets/Scenes/MainScene.unity`.
5. Press **Play** in the Unity Editor.

---

## 📖 Academic Documentation

The complete theoretical background, pedagogical analysis, and system specifications are available in the repository documentation folder (`/docs`).