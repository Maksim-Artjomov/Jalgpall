using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{

    public class Game 
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; private set; }

        public Game(Team homeTeam, Team awayTeam, Stadium stadium) // Конструктор для создания игры, где выдаём названия двум командам и название стадиону
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
        }

        public void Start() // Метод для запуска игры
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this); // Установка мяча на середину поля
            HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height); // Установка первой команды
            AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height); // Установка второй команды
        }
        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y); // Вычисляем абсолютные координаты игрока на поле
        }

        public (double, double) GetPositionForTeam(Team team, double x, double y) // Возвращение абсолютных координат игрока на поле для заданной команды
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y); // Если у HomeTeam координаты остаются без изменени, то используется GetPositionForAwayTeam
        }

        public (double, double) GetBallPositionForTeam(Team team) // Возвращение абсолютных координат мяча на поле для заданной команды, используя GetPositionForTeam
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy) // Устанавливка скорости мяча для заданной команды
        {
            if (team == HomeTeam)
            {
                Ball.SetSpeed(vx, vy);
            }
            else
            {
                Ball.SetSpeed(-vx, -vy);
            }
        }

        public void Move()
        {
            HomeTeam.Move();
            AwayTeam.Move();
            Ball.Move();
        }
    }
}
