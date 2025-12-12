# Space Shooter - Actividad 1 de la materia Diseño de Juegos 1

Un videojuego 2D tipo Space Shooter inspirado en el clásico arcade **Gradius**, desarrollado en Unity para Actividad 1 de la materia Diseño de Juegos 1.

## 📋 Descripción del Proyecto

Este proyecto implementa un juego de disparos espacial completo que presenta una nave espacial controlada por el jugador que batalla contra oleadas de enemigos mientras recolecta potenciadores para mejorar armas.

**Motor:** Unity 6000.3.0f1  
**Sistema de Entrada:** New Input System  
**Desarrollador:** Jorge Blacio

## ✨ Características Principales

### Mecánicas Base
- **Bucle de Juego Principal**: Menú principal, juego activo y estados de fin.
- **Controles del Jugador**: Movimiento y mecánicas de disparo usando el Input System.
- **Sistema de Enemigos**: Múltiples tipos de enemigos con movimiento basado en patrones y oleadas.
- **Sistema de Vidas**: El jugador comienza con 3 vidas, reaparece hasta que las pierda todas.

### Armas y Potenciadores
Tres tipos de armas distintos disponibles a través del sistema de potenciadores:
1. **Láser Básico**: Arma de proyectil estándar
2. **Láser Doble**: Arma de doble disparo para mayor daño
3. **Láser de Dispersión**: Sistema de proyectiles multidireccional para mayor cobertura

Generador dinámico de potenciadores que proporciona mejoras de armas durante el juego.

### Gestión de Enemigos
- **Tipos de Enemigos Diversos**: Múltiples configuraciones de enemigos con comportamientos únicos
- **Patrones de Movimiento**: Los enemigos siguen patrones de ruta predefinidos (patrulla, onda sinusoidal, vertical)
- **Generador de Enemigos**: Sistema de generación basado en oleadas con tasas de generación y patrones configurables
- **Detección de Colisiones**: Detección de impactos adecuada y destrucción de enemigos

### Pulido y UX
- **Gestor de UI**: Visualización de vidas.
- **Sistema de Audio**: Música de fondo para menú y juego con efectos de sonido
- **Retroalimentación Visual**: Efectos de explosión e indicadores visuales de destrucción

## 🎮 Cómo Jugar

1. **Iniciar el Juego**: Lanza el juego desde el menú principal
2. **Movimiento**: Usa las teclas de flecha o stick analógico para mover la nave espacial
3. **Disparar**: Mantén presionado el botón de disparo para disparar continuamente a los enemigos (Click, Espacio, o Buton Inferior en Gamepad)
4. **Recolectar Potenciadores**: Recolecta potenciadores para mejorar tus armas
5. **Sobrevivir**: Evita los proyectiles enemigos y derrota tantos enemigos como sea posible
6. **Fin del Juego**: El juego termina cuando se agotan todas las vidas

## 📁 Estructura del Proyecto

```
Assets/
├── Scripts/
│   ├── Game/
│   │   ├── GameManager.cs          # Controlador principal de estado y lógica del juego
│   │   └── UIManager.cs            # Gestión de actualizaciones y visualización de UI
│   ├── Player/
│   │   └── PlayerSpaceShip.cs      # Control y comportamiento de la nave espacial del jugador
│   ├── Enemy/
│   │   ├── Enemy.cs                # Clase base de enemigos y lógica
│   │   ├── EnemySpawner.cs         # Sistema de generación de oleadas
│   │   ├── PatternEnemy.cs         # Enemigo con patrones de movimiento
│   │   └── MovementPatterns.cs     # Definiciones de patrones (sinusoidal, circular, etc.)
│   ├── Weapons/
│   │   ├── WeaponSystem.cs         # Mecánicas principales de disparo de armas
│   │   ├── WeaponManager.cs        # Cambio de armas y mejoras
│   │   ├── BasicLaser.cs           # Implementación del arma básica
│   │   ├── DoubleLaser.cs          # Implementación del arma de doble disparo
│   │   ├── SpreadLaser.cs          # Arma multidireccional
│   │   ├── Projectile.cs           # Clase base de proyectiles
│   │   ├── WeaponPowerUp.cs        # Lógica de recogida de potenciadores
│   │   └── PowerUpSpawner.cs       # Sistema de generación de potenciadores
│   └── Effects/
│       └── [Efectos visuales y explosiones]
├── Prefabs/
│   └── [Plantillas de objetos de juego]
├── Scenes/
│   └── [Escenas y niveles del juego]
├── Sounds/
│   ├── Music/
│   └── SoundEffects/
├── Textures/
│   └── [Assets de sprites]
└── Materials/
    └── [Materiales del juego]
```

## 🛠️ Destacados de la Arquitectura

### Gestión de Estados
`GameManager` implementa un patrón singleton con estados de juego distintos:
- **MainMenu**: Estado inicial con interacción de menú
- **Playing**: Estado de juego activo
- **GameOver**: Estado final con opciones de reinicio

### Sistema de Armas
- Clases de armas modulares que heredan del sistema de armas base
- Cambio dinámico de armas a través de la recolección de potenciadores
- Cada tipo de arma tiene patrones de disparo y mecánicas únicas

### Sistema de Enemigos
- La clase base `Enemy` maneja el comportamiento común de enemigos
- `PatternEnemy` añade soporte para patrones de movimiento complejos
- `EnemySpawner` gestiona la generación de oleadas e intervalos de generación
- `MovementPatterns` proporciona implementaciones de movimiento reutilizables

### Manejo de Entrada
- Utiliza el nuevo Input System de Unity.
- Acciones de entrada configurables para movimiento y disparo
- Soporta entrada de teclado y gamepad

## 🎯 Pilares del Diseño del Juego

1. **Accesibilidad**: Fácil de aprender, difícil de dominar
2. **Progresión**: Los potenciadores de armas proporcionan retroalimentación clara de progresión
3. **Sentimiento Arcade**: Acción rápida con retroalimentación visual y de audio
4. **Escalabilidad**: El diseño modular permite la fácil adición de nuevos enemigos y armas

## 🚀 Mejoras Potenciales

- Tipos de armas adicionales y variedades de potenciadores
- Encuentros con jefes finales con patrones de ataque únicos
- Múltiples niveles de dificultad
- Sistema de tabla de puntuaciones
- Tipos de enemigos y comportamientos adicionales
- Obstáculos y peligros ambientales
- Fondo de desplazamiento paraláctico

## 📝 Requisitos

- **Versión de Unity**: 6000.3.0f1 o compatible
- **Sistema de Entrada**: Paquete New Input System
- **Pipeline de Renderizado**: Universal Render Pipeline (URP)

## 📄 Licencia

Este proyecto se crea con fines educativos como parte de Actividad 1 de la materia Diseño de Juegos 1.

---

**Creado por:** Jorge Blacio  
**Última Actualización:** Diciembre 2025
