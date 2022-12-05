# Game Manager
Keeps track of total points, prefabs, and 

````
GameManager singleton;
DifficultyLevel[10] levels;
GameObject gamePrefab;
int score;

void SplitGame(GameObject obj);
````

## Possible splits
Splitting a camera -> 

1st split -> x offset += width/2, x offset -= width/2
2nd split -> y offset += .5, y offset += 0, both height /= 2
3rd split (if 8) -> width /= 2, x offset += new width, x offset -= new width