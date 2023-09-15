using System;

namespace Jalgpall;

public class Player
{   // Väljud - Поля
    public string Name { get; } // Имя
    public double X { get; private set; } // Координата x для игрока
    public double Y { get; private set; } // Координата y для игрока
    private double _vx, _vy; // Расстояния игрока и мяча
    public Team? Team { get; set; } = null; // Команда, в которой играет игрок

    // Const для максимальной скорости игрока и скорости удара мяча
    private const double MaxSpeed = 5; // Максимальная скорость игрока
    private const double MaxKickSpeed = 25; // Максмальная скорость удара
    private const double BallKickDistance = 10; // Расстояние удара

    private Random _random = new Random(); // Генератор рандомных чисел




    // Konstruktorid - Конструкторы
    public Player(string name) // Конструктор, который мы приравниваем к имени (разминка).
    {
        Name = name; // Создание игрока с именем
    }
    // Конструктор для создания игрока с именем, координатами и привязкой к команде
    public Player(string name, double x, double y, Team team) // Конструктор, который задаёт игроку координаты и команду (начало игры).
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }

    public void SetPosition(double x, double y) // Метод для расстановки позиций игроков
    {
        X = x; // Позиция игрока по x
        Y = y; // Позиция игрока по y
    }
    
    public (double, double) GetAbsolutePosition() // Метод для получения абсолютной позиции игрока
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y); // Определение позиции команды
    }
    public double GetDistanceToBall() // Метод для вычисления расстояния до мяча по теореме пифагора
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    public void MoveTowardsBall() // Метод для перемещения игрока в направлении мяча
    {
        var ballPosition = Team!.GetBallPosition(); // Позиция мяча
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
        _vx = dx / ratio; // Путь
        _vy = dy / ratio; // Путь
    }
    public void Move() // Метод для выполнения движения игрока
    {
        if (Team.GetClosestPlayerToBall() != this) // Проверка игрока на близость к мячу
        {
            _vx = 0;
            _vy = 0;
        }

        if (GetDistanceToBall() < BallKickDistance) // Если игрок близко к мячу, то задаём скорость мяча
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(), // Скорость мяча при ударе
                MaxKickSpeed * (_random.NextDouble() - 0.5) // Скорость мяча после удара
                );
        }

        var newX = X + _vx; // Вычисляем новые координаты игрока
        var newY = Y + _vy; // Вычисляем новые координаты игрока
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2)) // Проверка на то, что игрок в поле
        {
            X = newX;
            Y = newY;
        }
        else // Если False, то оставляем игрока на том же месте
        {
            _vx = _vy = 0;
        }
    }
}