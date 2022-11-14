using Game_Library;
using System.Media;


Console.SetWindowSize(Game.WindowX + 1, Game.y + 1);
Console.CursorVisible = false;
string[] StartGame = {"","",
"          ***     *******         **           *******     *******                  ",
"         *   *       *           *  *          *      *       *                     ",
"        *            *          *    *         *       *      *                     ",
"          *          *         *      *        *       *      *                     ",
"            *        *        *        *       *******        *                     ",
"              *      *       ************      *    *         *                     ",
"              *      *      *            *     *     *        *                     ",
"         *   *       *     *              *    *      *       *                     ",
"          ***        *    *                *   *       *      *                     ",
"                                                                                    ",
"                                                                                    ",
"           ************            **                   *     *          *********  ",
"           *                      *  *                 * *   * *         *          ",
"           *                     *    *               *   * *   *        *          ",
"           *                    *      *             *     *     *       *          ",
"           *************       *        *           *             *      ******     ",
"           *           *      ************         *               *     *          ",
"           *           *     *            *       *                 *    *          ",
"           *          *     *              *     *                   *   *          ",
"            **********     *                *   *                     *  *********  ",
"                                                                                    ",
"                                                                                    ",
"                               Press Enter to start   <3                            ",
};
string[] GameOver = {"","",
"           ************            **                   *     *          *********  ",
"           *                      *  *                 * *   * *         *          ",
"           *                     *    *               *   * *   *        *          ",
"           *                    *      *             *     *     *       *          ",
"           *************       *        *           *             *      ******     ",
"           *           *      ************         *               *     *          ",
"           *           *     *            *       *                 *    *          ",
"           *          *     *              *     *                   *   *          ",
"            **********     *                *   *                     *  *********  ",
"                                                                                    ",
"                                                                                    ",
"                                                                                    ",
"                ****       *                  *    *********     *******            ",
"               *     *      *                *     *             *      *           ",
"              *       *      *              *      *             *       *          ",
"             *         *      *            *       *             *       *          ",
"             *         *       *          *        *             *********          ",
"             *         *        *        *         ******        *    *             ",
"             *         *         *      *          *             *     *            ",
"             *        *           *    *           *             *      *           ",
"               *     *             *  *            *             *       *          ",
"                ****                **             *********     *        *         ",
"                                                                                    ",
"                                                                                    ",
"                               Close the window <3                                  ",
"                                                                                    ",
"                                                                                    ",
"                                                                                    ",
};
SoundPlayer player = new SoundPlayer(@"D:\Рабочий стол\Sound_19341.wav");
player.PlayLooping();

for (int i = 0; i < StartGame.Length; i++)
{
    Console.WriteLine(StartGame[i]);
}
Console.ReadLine();
Console.Clear();
Console.SetCursorPosition(83, 5);
Console.WriteLine("Score");
Game.walls = new Walls(Game.x, Game.y, '#');
Game.player = new Player(Game.x / 2, Game.y - 1, 'O');
Game.bricks = new Bricks(1, 1, '-');
Thread thread = new Thread(MoveBricks);
Thread score = new Thread(Score);
score.Start();
thread.Start();
Game.StartGame(Game.player);
player = new SoundPlayer(@"D:\Рабочий стол\jg-032316-sfx-video-game-game-over-3.wav");
player.Play();
Console.Clear();

Console.SetWindowSize(Game.WindowX + 1, Game.y + 7);
for (int i = 0; i < GameOver.Length; i++)
{
    Console.WriteLine(GameOver[i]);
}
Console.SetCursorPosition(27, 24);
Console.Write($"Your Score: {Count.x}");
Console.ReadLine();


void MoveBricks()


{
    int time = 200;
    int tochingthewall = 0;
    while (true)
    {
        Game.bricks.Move();
        Thread.Sleep(time);
        if (Game.bricks.IsHit(Game.player.GetPoint()) || Game.walls.Ishit(Game.player.GetPoint()))
        {
            Console.Clear();
            for (int i = 0; i < GameOver.Length; i++)
            {
                Console.WriteLine(GameOver[i]);

            }
            Console.SetWindowSize(Game.WindowX + 1, Game.y + 7);
            Console.SetCursorPosition(27, 24);
            Console.Write("     ");
            Console.SetCursorPosition(27, 24);
            Console.Write($"Your Score: {Count.x}");
            player = new SoundPlayer(@"D:\Рабочий стол\jg-032316-sfx-video-game-game-over-3.wav");
            player.Play();
            return;
        }
        if (Game.bricks.IsHitOfWalls(Game.walls.walls))
        {
            Game.bricks.GenerateNew();
            tochingthewall++;
        }
        if (time < 50)
        {
            tochingthewall = 0;
        }
        if (tochingthewall == 3)
        {
            time -= 15;
            tochingthewall = 0;
        }

    }

}

void Score()
{
    while (true)
    {
        Console.SetCursorPosition(85, 6);
        Console.Write(Count.x);
        Thread.Sleep(1000);
        Count.x++;
        if (Game.bricks.IsHit(Game.player.GetPoint()) || Game.walls.Ishit(Game.player.GetPoint()))
        {
            Console.SetWindowSize(Game.WindowX + 1, Game.y + 7);
            Console.SetCursorPosition(27, 24);
            Console.Write($"Your Score: {Count.x}");
            return;
        }
    }



}