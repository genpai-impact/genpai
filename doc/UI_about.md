# UI_about

## UI Canvas层

### `Role_Information` 角色信息

#### `Art_R` 角色美术
美术负责
#### `Attribute_R` 角色属性
角色属性为角色固有的属性
##### `HP_R` 生命值
###### `image_HP_R` 角色HP的UI图标
由多个UI图标叠加而成  
策划：  
设计HP的UI，例如护甲时的显示、满血状态显示、非满血状态显示的UI  
美术：  
UI相关制作  
可参考后续的"UI布局-参考"一图  
程序：  
通过脚本实现UI状态的转变  
具体：脚本获取并控制颜色or图片   
###### `text_HP_R` 角色HP的数值显示

##### `Energy_R` 角色能量 
能量是角色相较于怪物`Monster`和`BOSS`其他两个基本单位独有的属性  
###### `image_EN_R` 角色能量的UI图标

###### `text_EN_R` 角色能量的数值显示

##### `Element_R` _角色元素

角色元素即为角色攻击时的元素伤害类型  
具体内容后续完善  
#### `State_R` 角色状态
角色状态
##### `Buff_Icon_R` buff图标
美术：  
Buff的UI图标制作，可参考策划的需求文档  
程序：  
通过控制脚本，生成Buff栏并控制，使图标准确触发即可  
##### `Buff_RemainingRound_R`
buff的剩余回合，程序来控制，同时可能牵扯到部分buff与层数相关  
请多注意  
#### `AttackAttributes_R(Reserved)`  角色攻击方式（保留）
目前不涉及到，后续可能归并至角色属性中，略