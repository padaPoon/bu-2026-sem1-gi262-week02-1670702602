using System;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        // Unity does not serialize multidimensional arrays, so store the map as a
        // one-dimensional array and index it by row and column when needed.
        public string[] saveItemMap = new string[9] {
            " ", "Soda", " ",
            " ", " ", " ",
            " ", " ", "Food",
        };

        // 1. declare Players variable
        public GameObject[] Players;

        // 7. declare Exit variable
        public GameObject Exit;


        public void Start()
        {
            // 1. random player at the position <0, 0> map
            {
                int r = UnityEngine.Random.Range(0, Players.Length);
                Instantiate(Players[r], new Vector2(0, 0), Quaternion.identity);
            }

            // 2. create obstacles
            for (int posX = 0; posX < 5; posX++)
            {
                GameObject toInstantiate = wallTiles[UnityEngine.Random.Range(0, wallTiles.Length)];
                GameObject obstacle = Instantiate(toInstantiate, new Vector2(posX, 2), Quaternion.identity);
                obstacle.name = "Obstacle";
            }

            // 3. create floor
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[r], new Vector2(x, y), Quaternion.identity);
                    tile.name = "Floor" + x + "_" + y;
                }
            }
            // 4. create walls
            for (int y = -1; y < rows + 1; y++)
            {
                for (int x = -1; x < columns + 1; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        int r = UnityEngine.Random.Range(0, wallTiles.Length);
                        GameObject tile = Instantiate(wallTiles[r], new Vector2(x, y), Quaternion.identity);
                        tile.name = "Wall" + x + "_" + y;
                    }
                }
            }
            // 5. random foods
            int numberOfFood = UnityEngine.Random.Range(2, 3);
            for (int i = 0; i < numberOfFood; i++)
            {
                int FoodXpos = UnityEngine.Random.Range(0, columns);
                int FoodYpos = UnityEngine.Random.Range(0, rows);
                int r = UnityEngine.Random.Range(0, foodTiles.Length);
                Instantiate(foodTiles[r], new Vector2(FoodXpos, FoodYpos), Quaternion.identity);

            }
            // 6. generate item along with the saveItemMap

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    string item = saveItemMap[y * 3 + x];
                    if (!string.IsNullOrEmpty(item) && item != " ")
                    {
                        foreach (var food in foodTiles)
                        {
                            if (food.name == item)
                            {
                                Instantiate(food, new Vector2(x, y), Quaternion.identity);
                            }
                        }

                    }
                }
            }

            // 7. place exit
            Instantiate(Exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }
    }
}