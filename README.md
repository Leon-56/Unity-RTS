# Unity-RTS　
使用unity、C#开发的即时战略游戏，主要参考[GameFrameWork](https://github.com/EllanJiang/GameFramework)、《设计模式与游戏完美开发》，对GameFrameWork进行简化运用，使用各种设计模式进行配合，实现整个游戏的框架。

## 引擎工具　　
Unity版本： 2019.3.0f6  

## 游戏模块  
- 全局配置 (Config) - 存储只读游戏配置数据。  
- 实体 (Entity) - 场景中动态创建的物体，使用结束后不立刻销毁。  
- 事件 (Event) - 游戏逻辑监听、抛出事件的机制。  
- 有限状态机 (FSM) - 提供创建、使用和销毁有限状态机的功能。  
- 对象池 (Object Pool) - 提供对象缓存池的功能，避免频繁地创建和销毁各种游戏对象，提高游戏性能。  
- 资源 (Resource) - 资源加载模块。  
- 配置 (Setting) - 以键值对的形式存储玩家数据，对 UnityEngine.PlayerPrefs 进行封装。  
- 界面 (UI) - 提供管理界面和界面组的功能，如显示隐藏界面、激活界面、改变界面层级等。不论是 Unity 内置的 uGUI 还是其它类型的 UI 插件（如 NGUI），只要派生自 UIFormLogic 类并实现自己的界面类即可使用。界面使用结束后可以不立刻销毁，从而等待下一次重新使用。  
## 游戏截图
