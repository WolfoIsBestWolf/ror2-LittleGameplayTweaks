Fixes wrong dlc spawn pools being chosen often.

Hopoo Games implementation for DLC spawn pools does not work properly with multiple DLCs.\
Gearbox, didn't fix this.

A lot of stages randomly can not spawn SotV and SotS content together.\
SotV and Sots stages do not have this issue.\
This makes Void items and Halcyon Shrines rarer than intended. Not by too much but a noticeable amount.

This is also the reason why sometimes Magma Worms are on Sky Meadow, or Elder Lemurians instead of Gups in Grove.

All non sots Stage 3s,4s,5s also can't spawn Sots interactables.\
Not for any intended reason, they just somehow gave up half way through adding them. 

This mod also makes Titanic Plains and Distant Roost not twice as common as other stage 1s.\
Plains and Roost are in the StagePool twice due to their Variants.\
Hopoo/Gearbox seem to change their mind often whether whether both variants should have a weight of 1 or a lower weight.\
They now have a weight of 0.75 so still 50% more common, the variants are quite different.


The followin problems are also fixed:
```
- Disturbed Impact : 	Uses Titanic Plains interactables instead of the correct pool
- Golden Dieback : 		Uses Treeborn Colony interactables instead of correct pool.
- Bulwarks Ambry 02 : 	Uses wrong Bulwarks Ambry monster spawn pool

- Shattered Abodes : 	Has Stage 1 Overgrown Printers. Unlike other stage 1s
- Treeborn Colony : 	Spawns Radar Scanners even after unlocking log.

- Titanic Plains : 	Never uses Sotv+Sots monster pool (Loop Halcyonite)
- Siphoned Forest : 	Never uses Sotv+Sots monster pool (Loop Scorch Wurm)
- Verdant Falls : 	Never uses Sotv+Sots monster pool (Children)
- Aphelian Sanctuary : 	Never uses Sotv+Sots monster pool (No differences)

- Rallypoint Delta : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Scorched Acres : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Sulfur Pools : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Abyssal Depths : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Sirens Call : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Sundered Grove : 	Never uses Sotv+Sots interactables pool (Halc shrines)
- Sky Meadow : 		Never uses Sotv+Sots interactables pool (Halc shrines)




