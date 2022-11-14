using System.Media;

namespace Game_Library
{
    public class Count
    {
        public static int x = 0;
    }

    public class Game
    {
        public static readonly int WindowX = 90;
        public static readonly int x = 80;
        public static readonly int y = 26;
        public static Walls? walls;
        public static Player? player;
        public static Bricks? bricks;
        public static void StartGame(Player? player)
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    player.Rotation(key.Key);
                    player.Move();
                    if (walls.Ishit(player.GetPoint()) || bricks.IsHit(player.GetPoint()))
                    {
                        Console.SetCursorPosition(29, 26);
                        Console.Write($"Your Score: {Count.x}");
                        return;
                    }
                }
            }
        }

    }

    public struct Point
    {
        public int x;
        public int y;
        public char symbol;

        public static implicit operator Point((int, int, char) value) =>
            new Point { x = value.Item1, y = value.Item2, symbol = value.Item3 };
        public static bool operator ==(Point a, Point b) =>
            (a.x == b.x && a.y == b.y);
        public static bool operator !=(Point a, Point b) =>
            (a.x != b.x || a.y != b.y);

        public void Draw()
        {
            DrawPoint(symbol);
        }

        public void Clear()
        {
            DrawPoint(' ');
        }

        public void DrawPoint(char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

    }

    public class Walls
    {
        private char symbol;
        public List<Point> walls = new List<Point>();
        public Walls(int x, int y, char symbol)
        {
            this.symbol = symbol;
            DrawHorizontal(Game.WindowX, 0);
            DrawHorizontal(Game.WindowX, y);
            DrawVertical(0, y);
            DrawVertical(x, y);
            DrawVertical(Game.WindowX, y);
        }

        private void DrawHorizontal(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                Point p = (i, y, symbol);
                p.Draw();
                walls.Add(p);
            }
        }

        private void DrawVertical(int x, int y)
        {
            for (int i = 0; i < y; i++)
            {
                Point p = (x, i, symbol);
                p.Draw();
                walls.Add(p);
            }
        }

        public bool Ishit(Point p)
        {
            foreach (var w in walls)
            {
                if (p == w)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Player
    {
        private static Direction direction;
        private int step = 1;
        private List<Point> players = new List<Point>();
        public static Point NextPos;
        private Point LastPos;
        public static Point PlayerPos;
        bool rotate = true;

        public Player(int x, int y, char symbol)
        {
            direction = Direction.Right;
            NextPos = (x, y, symbol);
            NextPos.Draw();


        }

        public void Move()
        {
            LastPos = NextPos;
            LastPos.Clear();
            NextPos = GetNextPoint();
            NextPos.Draw();
            rotate = true;
        }

        public Point GetPoint() => NextPos;

        public Point GetNextPoint()
        {
            Point player = GetPoint();
            switch (direction)
            {
                case Direction.Left:
                    player.x -= step;
                    break;
                case Direction.Right:
                    player.x += step;
                    break;
                default: break;

            }
            return player;
        }

        public void Rotation(ConsoleKey key)
        {
            if (rotate)
            {
                switch (direction)
                {
                    case Direction.Left:
                        if (key == ConsoleKey.RightArrow)
                            direction = Direction.Right;
                        break;
                    case Direction.Right:
                        if (key == ConsoleKey.LeftArrow)
                            direction = Direction.Left;
                        break;
                }
                rotate = false;
            }
        }
    }

    public class Bricks
    {
        private List<Point> points = new List<Point>();
        Random random = new Random();
        Point brick;
        private char _symbol;
        private int _x;
        private int _y;
        int _step;
        public Bricks(int x, int y, char symbol)
        {
            _x = x;
            _y = y;
            _symbol = symbol;
            for (int i = 1; i <= 18; i++)
            {
                brick = (x, y, symbol);
                points.Add(brick);
                brick.Draw();
                x++;
                if (i % 3 == 0)
                {
                    x += 12;
                }
            }


        }

        public void GenerateNew()
        {
            int x = _x; int y = _y;
            if (points.Count != 76)
            {
                brick = (x, y, _symbol);
                points.Add(brick);
            }
            for (int i = 0; i < points.Count; i++)
            {

                x = random.Next(1, 80);
                if (points[i].x == x)
                {
                    i--;
                    continue;
                }
                points[i].Clear();
                points[i] = (x, y, _symbol);
                points[i].Draw();

            }


        }


        public bool IsHitOfWalls(List<Point> list)
        {
            foreach (Point p in list)
            {
                if (points[0].y == 25)
                {
                    return true;
                }

            }
            return false;
        }

        public void Move()
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].Clear();
                points[i] = GetNextPoint(points[i]);
                points[i].Draw();


            }
        }

        private Point GetNextPoint(Point p)
        {
            p.y++;
            return p;
        }

        public bool IsHit(Point p)
        {
            foreach (var w in points)
            {
                if (p == w)
                {
                    return true;
                }
            }
            return false;
        }

    }

    enum Direction
    {
        Left,
        Right
    }
}