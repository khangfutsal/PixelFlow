# PixelFlow Scripts - Quick Reference

## 📁 All Created Files (16 total)

### Core Scripts (in `Assets/PixelFlow/Scripts/`)
1. ✅ `GemColor.cs` - Color enum (Red, Blue, Green, Yellow, Purple)
2. ✅ `Gem.cs` - Gem with destruction animation
3. ✅ `Projectile.cs` - Projectile movement and collision
4. ✅ `PigJar.cs` - Main pig shooter with raycast
5. ✅ `SlotQueue.cs` - Queue management system
6. ✅ `SlotBuffer.cs` - Buffer with alert blinking
7. ✅ `Dispenser.cs` - Item spawning controller
8. ✅ `Conveyor.cs` - Waypoint-based movement
9. ✅ `PigJarClickHandler.cs` - Click interaction helper
10. ✅ `GameManager.cs` - Main game controller (Singleton)

### Data Scripts (in `Assets/PixelFlow/Scripts/Data/`)
11. ✅ `LevelConfigData.cs` - ScriptableObject for config
12. ✅ `LevelRenderData.cs` - JSON level structure

### UI Scripts (in `Assets/PixelFlow/Scripts/UI/`)
13. ✅ `WinUI.cs` - Win screen with animations
14. ✅ `LoseUI.cs` - Lose screen with retry
15. ✅ `UIManager.cs` - UI controller and HUD

### Data Files
16. ✅ `Assets/Resources/Levels/Level1.json` - Sample level

### Documentation
17. ✅ `Assets/PixelFlow/README.md` - Complete setup guide

---

## 🎯 Quick Setup Checklist

### 1. Create Prefabs
- [ ] Gem prefab (with SpriteRenderer, CircleCollider2D, Gem.cs)
- [ ] PigJar prefab (with SpriteRenderer, Collider2D, PigJar.cs, PigJarClickHandler.cs)
- [ ] Projectile prefab (with SpriteRenderer, CircleCollider2D, Projectile.cs)

### 2. Create Layer
- [ ] Add "Gem" layer in Project Settings

### 3. Scene Setup
- [ ] GameManager GameObject with GameManager.cs
- [ ] Conveyor GameObject with Conveyor.cs + waypoints
- [ ] Dispenser GameObject with Dispenser.cs
- [ ] 3-5 SlotQueue GameObjects with SlotQueue.cs
- [ ] 5 SlotBuffer GameObjects with SlotBuffer.cs
- [ ] UI Canvas with Win/Lose panels

### 4. Assign References
- [ ] GameManager: Assign all prefabs and references
- [ ] Conveyor: Assign dispenser and waypoints
- [ ] Dispenser: Assign SlotQueues and SlotBuffers
- [ ] UI panels: Assign buttons and text elements

---

## 🎮 How to Use

1. **Open Unity** at `d:\unity2d\PixelFlowGit`
2. **Read** `Assets/PixelFlow/README.md` for detailed setup
3. **Create prefabs** as described
4. **Setup scene** with all GameObjects
5. **Press Play** and click PigJars to test!

---

## 📝 Key Namespaces

```csharp
using PixelFlow;           // Core game objects
using PixelFlow.Data;      // Data structures
using PixelFlow.UI;        // UI components
```

---

## 🔧 Important Settings

### PigJar
- Shoot Interval: 1.0s
- Raycast Distance: 20
- Gem Layer: Set to "Gem" layer mask

### Conveyor
- Move Speed: 2
- Shoot Direction: (1, 0, 0)

### Dispenser
- Limit Count: 5

### Animations
- Gem Destroy: 0.2s
- UI Fade In: 0.5s

---

## 🎨 Color Enum Values

```
0 = Red
1 = Blue
2 = Green
3 = Yellow
4 = Purple
```

Use these values in JSON level files and when setting colors in Inspector.
