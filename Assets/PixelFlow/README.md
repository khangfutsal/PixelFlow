# PixelFlow - Unity 2D Game Prototype

## 📋 Overview
PixelFlow is a Unity 2D puzzle game where pigs on a conveyor belt shoot colored projectiles to destroy matching gems on a board.

## 📁 Project Structure

```
Assets/
├── PixelFlow/
│   └── Scripts/
│       ├── Core/
│       │   ├── GemColor.cs              # Enum for gem colors
│       │   ├── Gem.cs                   # Gem behavior with destruction
│       │   ├── PigJar.cs                # Pig shooter with raycast
│       │   ├── Projectile.cs            # Projectile movement
│       │   ├── SlotQueue.cs             # Queue management
│       │   ├── SlotBuffer.cs            # Buffer with alerts
│       │   ├── Dispenser.cs             # Item spawning system
│       │   └── Conveyor.cs              # Conveyor movement
│       ├── Data/
│       │   ├── LevelConfigData.cs       # ScriptableObject config
│       │   └── LevelRenderData.cs       # JSON level structure
│       ├── UI/
│       │   ├── WinUI.cs                 # Win screen
│       │   ├── LoseUI.cs                # Lose screen
│       │   └── UIManager.cs             # UI controller
│       ├── GameManager.cs               # Main game controller
│       └── PigJarClickHandler.cs        # Click interaction
└── Resources/
    └── Levels/
        └── Level1.json                   # Sample level data
```

## 🎮 Game Mechanics

### Win Condition
- Destroy all gems on the board

### Lose Condition
- All 5 SlotBuffer slots are filled (triggers red blinking alert)

### Core Systems
1. **Conveyor Belt**: Moves PigJars along waypoints
2. **PigJar Shooting**: Raycasts to find matching colored gems
3. **Gem Destruction**: Scale animation (0.2s up then down)
4. **Dispenser**: Limits 5 PigJars on conveyor simultaneously

## 🛠️ Setup Instructions

### 1. Create Prefabs

#### Gem Prefab
1. Create empty GameObject → name it "Gem"
2. Add `SpriteRenderer` component
   - Sprite: Circle or gem sprite
   - Material: Default
3. Add `CircleCollider2D`
   - Is Trigger: ✓
   - Layer: Set to "Gem" layer
4. Add `Gem.cs` script
5. Save as prefab in `Assets/PixelFlow/Prefabs/Gem.prefab`

#### PigJar Prefab
1. Create empty GameObject → name it "PigJar"
2. Add `SpriteRenderer` component
   - Sprite: Capsule or pig sprite
3. Add `CircleCollider2D` or `BoxCollider2D`
4. Add `PigJar.cs` script
   - Assign Projectile Prefab
   - Set Shoot Interval: 1
   - Set Raycast Distance: 20
   - Set Gem Layer mask
5. Add `PigJarClickHandler.cs` script
6. Create child GameObject "ShootPoint"
   - Position it where projectiles should spawn
   - Assign to PigJar's Shoot Point field
7. Save as prefab in `Assets/PixelFlow/Prefabs/PigJar.prefab`

#### Projectile Prefab
1. Create empty GameObject → name it "Projectile"
2. Add `SpriteRenderer` component
   - Sprite: Small circle
3. Add `CircleCollider2D`
   - Is Trigger: ✓
4. Add `Projectile.cs` script
   - Speed: 10
   - Lifetime: 5
5. Save as prefab in `Assets/PixelFlow/Prefabs/Projectile.prefab`

### 2. Create Layers
1. Edit → Project Settings → Tags and Layers
2. Add layer: "Gem"
3. Assign Gem prefab to "Gem" layer

### 3. Scene Setup

#### GameManager
1. Create empty GameObject → name it "GameManager"
2. Add `GameManager.cs` script
3. Assign references:
   - Gem Prefab
   - PigJar Prefab
   - Current Level Id: 1

#### Conveyor
1. Create empty GameObject → name it "Conveyor"
2. Add `Conveyor.cs` script
3. In Inspector, set:
   - Move Speed: 2
   - Shoot Direction: (1, 0, 0) for right
4. Create waypoints:
   - Create child GameObjects: "Waypoint_0", "Waypoint_1", etc.
   - Position them along the conveyor path
   - Add to Conveyor's Waypoints list
5. Assign to GameManager's Conveyor field

#### Dispenser
1. Create empty GameObject → name it "Dispenser"
2. Add `Dispenser.cs` script
3. Assign PigJar Prefab
4. Set Limit Count Dispenser: 5
5. Assign to GameManager's Dispenser field

#### SlotQueues (Create 3-5)
1. Create empty GameObject → name it "SlotQueue_Red"
2. Add `SlotQueue.cs` script
3. Set Queue Color (0=Red, 1=Blue, 2=Green, etc.)
4. Position in scene
5. Repeat for other colors
6. Add all to GameManager's Slot Queues list

#### SlotBuffers (Create 5)
1. Create GameObject with SpriteRenderer → name it "SlotBuffer_1"
2. Add `SlotBuffer.cs` script
3. Assign Sprite Renderer
4. Set Normal Color: White
5. Set Alert Color: Red
6. Position near conveyor end
7. Repeat for 5 total buffers
8. Add all to Dispenser's L_slotBuffer list

#### UI Setup
1. Create Canvas → name it "UI Canvas"
2. Create Panel → name it "WinPanel"
   - Add `WinUI.cs` script
   - Add CanvasGroup component
   - Create child Text (TMP) for reward
   - Create child Buttons: "Restart", "Next Level"
   - Assign references in WinUI script
3. Create Panel → name it "LosePanel"
   - Add `LoseUI.cs` script
   - Add CanvasGroup component
   - Create child Button: "Retry"
   - Assign references in LoseUI script
4. Create empty GameObject → name it "UIManager"
   - Add `UIManager.cs` script
   - Assign Win/Lose panels
   - Create HUD Text elements for dispenser count
5. Assign UI panels to GameManager

### 4. Create Level Config (Optional)
1. Right-click in Project → Create → PixelFlow → Level Config Data
2. Set values:
   - ID: 1
   - Limit Count Dispenser: 5
   - Reward Coin: 100
3. Assign to GameManager's Level Config field

## 🎯 How to Play

1. Press Play in Unity
2. Click on PigJars in SlotQueues (they must be `isFree = true`)
3. PigJar moves onto conveyor
4. PigJar automatically shoots matching colored gems
5. Destroy all gems to win
6. Don't let all 5 buffer slots fill or you lose!

## 📝 Level Data Format (JSON)

```json
{
  "id": 1,
  "slotQueues": [
    {
      "queueColor": 0,
      "pigJars": [
        { "id": 1, "color": 0, "count": 3 }
      ]
    }
  ],
  "gems": [
    { "id": 1, "position": { "x": 5, "y": 2, "z": 0 }, "color": 0 }
  ]
}
```

### Color Enum Values
- 0 = Red
- 1 = Blue
- 2 = Green
- 3 = Yellow
- 4 = Purple

## 🔧 Customization

### Adjust Shooting
- `PigJar.cs` → `shootInterval`: Time between shots
- `PigJar.cs` → `raycastDistance`: How far to detect gems

### Adjust Movement
- `Conveyor.cs` → `moveSpeed`: Conveyor belt speed

### Adjust Animations
- `Gem.cs` → `destroyAnimationDuration`: Gem destruction time
- `PigJar.cs` → Animation coroutines for custom effects

### Adjust Difficulty
- `Dispenser.cs` → `limitCountDispenser`: Max dispenser uses
- Modify `Level1.json` → Add more gems or change colors

## 🐛 Troubleshooting

### PigJars not shooting
- Check Gem layer is set correctly
- Verify Raycast Distance is sufficient
- Ensure Shoot Direction matches conveyor orientation
- Check Projectile Prefab is assigned

### Gems not destroying
- Verify Gem has CircleCollider2D with "Is Trigger" enabled
- Check Projectile has matching color
- Ensure Gem layer matches PigJar's Gem Layer mask

### Click not working
- Verify PigJarClickHandler is attached
- Check PigJar has a Collider2D component
- Ensure Camera is set to Orthographic for 2D

### Win/Lose UI not showing
- Check UI panels are assigned in GameManager
- Verify CanvasGroup is attached to panels
- Ensure Canvas is in Screen Space - Overlay mode

## 📚 Script Dependencies

```
GameManager (Singleton)
├── Dispenser
│   ├── SlotQueue (multiple)
│   │   └── PigJar (queue)
│   └── SlotBuffer (5 total)
├── Conveyor
│   └── PigJar (active)
│       └── Projectile (spawned)
└── Gem (multiple)
```

## 🎨 Next Steps

1. **Add Sprites**: Replace colored shapes with actual sprites
2. **Add Animations**: Create Animator Controllers for PigJar states
3. **Add Sound**: Add AudioSource for shooting, destruction, win/lose
4. **Add Particles**: Gem destruction effects, conveyor steam
5. **Add More Levels**: Create Level2.json, Level3.json, etc.
6. **Add Scoring**: Track time, accuracy, combo multipliers
7. **Add Power-ups**: Special PigJars with abilities

## 📄 License
This is a prototype for educational purposes.
