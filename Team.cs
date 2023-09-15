using Jalgpall; // Импорт пространства имен Jalgpall.

using System;
using System.Collections.Generic;

namespace Jalgpall
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>(); // Список объектов типа Player в команде

        public string Name { get; private set; } // Имя

        public Game Game { get; set; } // Хранение объекта игры, в которой участвует команда

        public Team(string name) // Конструктор для создания команды с заданным именем
        {
            Name = name;
        }

        public void StartGame(int width, int height) // Метод для начала игры с заданными размерами поля (шириной и высотой)
        {
            Random rnd = new Random();
            foreach (var player in Players)
            {
                // Устанавливаем случайные координаты для каждого игрока на поле
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                );
            }
        }

        public void AddPlayer(Player player) // Метод для добавления игрока в команду
        {
            if (player.Team != null) return; // Проверка на принадлежность грока к другой команде

            Players.Add(player); // Добавляем игрока в список игроков команды
            player.Team = this; // Ставим игроку команду
        }
        
        public (double, double) GetBallPosition() // Метод для получения текущей позиции мяча
        {
            return Game.GetBallPositionForTeam(this);
        }

        public void SetBallSpeed(double vx, double vy) // Метод для установки скорости мяча для команды по x и y
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall() // Метод для определения близости игрока к мячу
        {
            Player closestPlayer = Players[0]; // Берём промежуточного игрока из списка
            double bestDistance = Double.MaxValue; // Создаём ближайшую дистанцию
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall(); // Вычисляем расстояние от игрока до мяча

                if (distance < bestDistance) // Если расстояние меньше, чем у предыдущего ближайшего игрока, обновляем значения
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer; // Возвращаем ближайшего игрока к мячу
        }

        
        public void Move() // Метод для выполнения движения команды
        {
            GetClosestPlayerToBall().MoveTowardsBall(); // Вызываем метод MoveTowardsBall() для передвежение ближайшего игрока к мячу

            Players.ForEach(player => player.Move()); // Вызываем метод Move() для всех игроков в команде
        }
    }
}
