using System;
using Controllers;
using Models;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TechnologicalUI
{
    public class Moderator
    {
        private readonly UserInfoController _userInfoController;
        private readonly AvailableDealsController _availableDealsController;
        private readonly PlayerController _playerController;
        private readonly TeamController _teamController;
        
        public Moderator(UserInfoController userInfoController, AvailableDealsController availableDealsController,
            PlayerController playerController, TeamController teamController)
        {
            _userInfoController = userInfoController;
            _availableDealsController = availableDealsController;
            _playerController = playerController;
            _teamController = teamController;
        }

        public void Menu()
        {
            var check = -1;
            while (check != 0)
            {
                Console.WriteLine
                (
                    "Меню\n" +
                    "1 - Просмотреть всех пользователей\n" +
                    "2 - Добавить пользователя\n" +
                    "3 - Удалить пользователя\n" +
                    "4 - Обновить информацию о пользователе\n" +
                    "5 - Просмотреть все сделки\n" +
                    "6 - Удалить сделку\n" +
                    "7 - Провести сделку\n" +
                    "0 - Выход"
                );

                check = Convert.ToInt32(Console.ReadLine());
                switch (check)
                {
                    case 0:
                        return;
                    case 1:
                        GetUsers();
                        break;
                    case 2:
                        AddUser();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    case 4:
                        UpdateUser();
                        break;
                    case 5:
                        GetAllDeals();
                        break;
                    case 6:
                        DeleteDeal();
                        break;
                    case 7: 
                        MakeDeal();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод\n");
                        break;
                }
            }
        }
        
        private void GetUsers()
        {
            var users = _userInfoController.GetAll();
            foreach (var user in users)
            {
                WriteUserToConsole(user);
            }
        }

        private void AddUser()
        {
            var user = InputUserInfo();
            var result = _userInfoController.Add(user);
            if (!result)
            {
                Console.WriteLine("Не удалось добавить пользователя");
            }
        }

        private void DeleteUser()
        {
            Console.WriteLine("Введите id пользователя");
            var id = Convert.ToInt32(Console.ReadLine());
            var result = _userInfoController.Delete(id);
            switch (result)
            {
                case StatusCode.BadRequest:
                    Console.WriteLine("Не удалось удалить пользователя");
                    break;
                case StatusCode.NotFound:
                    Console.WriteLine("Не удалось найти пользователя");
                    break;
            }
        }

        private void UpdateUser()
        {
            Console.WriteLine("Введите id пользователя");
            var id = Convert.ToInt32(Console.ReadLine());
            var user = InputUserInfo();
            user.Id = id;
            var result = _userInfoController.Update(user);
            if (!result)
            {
                Console.WriteLine("Не удалось обновить информацию о пользователе");
            }
        }

        private void GetAllDeals()
        {
            var deals = _availableDealsController.GetAll();
            if (deals == null)
            {
                Console.WriteLine("Ничего не найдено");
                return;
            }
            foreach (var deal in deals)
            {
                WriteDealToConsole(deal);
            }
        }

        private void DeleteDeal()
        {
            Console.WriteLine("Введите id сделки");
            var id = Convert.ToInt32(Console.ReadLine());
            var result = _availableDealsController.Delete(id);
            switch (result)
            {
                case StatusCode.BadRequest:
                    Console.WriteLine("Не удалось удалить сделку");
                    break;
                case StatusCode.NotFound:
                    Console.WriteLine("Не удалось найти сделку");
                    break;
            }
        }

        private void MakeDeal()
        {
            Console.WriteLine("Введите id сделки");
            var id = Convert.ToInt32(Console.ReadLine());
            
            var deal = _availableDealsController.GetDealById(id);
            if (deal == null)
            {
                Console.WriteLine("Не удалось найти сделку");
                return;
            }
            
            var newTeam = _teamController.FindTeamByManagement((int)deal.FrommanagementId);
            if (newTeam == null)
            {
                Console.WriteLine("Не удалось найти новую команду");
                return;
            }
            
            var lastTeam = _teamController.FindTeamByManagement((int)deal.TomanagementId);
            if (lastTeam == null)
            {
                Console.WriteLine("Не удалось найти текущую команду");
                return;
            }
            
            var player = _playerController.FindPlayerById((int)deal.PlayerId);
            if (player == null)
            {
                Console.WriteLine("Не удалось найти игрока");
                return;
            }
            
            if (!CheckPossibilityToBuy(newTeam, deal.Cost))
            {
                Console.WriteLine("Баланс команды меньше необходимой суммы");
                return;
            }
            
            UpdateTeamBalance(lastTeam, newTeam, deal.Cost);
            UpdatePlayerTeam(player, newTeam.TeamId);
            _availableDealsController.Delete(id);
            Console.WriteLine("Сделка проведена успешно");
        }

        private bool UpdatePlayerTeam(PlayerBL player, int team)
        {
            player.TeamId = team;
            return _playerController.Update(player);
        }

        private bool UpdateTeamBalance(TeamBL firstTeam, TeamBL secondTeam, int cost)
        {
            firstTeam.Balance += cost;
            secondTeam.Balance -= cost;
            if (!_teamController.Update(firstTeam))
            {
                return false;
            }
            return _teamController.Update(secondTeam);   
        }

        private bool CheckPossibilityToBuy(TeamBL team, int cost)
        {
            return cost < team.Balance;
        }

        private UserInfoBL InputUserInfo()
        {
            var user = new UserInfoBL();
            Console.WriteLine("Введите Login");
            user.Login = Console.ReadLine();
            Console.WriteLine("Введите Password");
            user.Hash = Console.ReadLine();
            Console.WriteLine("Введите Permissions");
            user.Permission = Convert.ToInt32(Console.ReadLine());
            return user;
        }

        private AvailabledealBL InputDeal()
        {
            var deal = new AvailabledealBL();
            Console.WriteLine("Введите Id");
            deal.Id = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Введите PlayerId");
            deal.PlayerId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите TomanagementId");
            deal.TomanagementId = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Введите FrommanagementId");
            deal.FrommanagementId = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Введите цену");
            deal.Cost = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Введите статус");
            deal.Status = Convert.ToInt32(Console.ReadLine());

            return deal;
        }

        private void WriteUserToConsole(UserInfoBL user)
        {
            Console.Write(user.Id);
            Console.Write(" ");
            
            Console.Write(user.Login);
            Console.Write(" ");
            
            Console.Write(user.Hash);
            Console.Write(" ");
            
            Console.Write(user.Permission);
            Console.Write(" ");
            
            Console.WriteLine();
        }

        private void WriteDealToConsole(AvailabledealBL deal)
        {
            Console.Write(deal.Id);
            Console.Write(" ");
            
            Console.Write(deal.PlayerId);
            Console.Write(" ");
            
            Console.Write(deal.TomanagementId);
            Console.Write(" ");
            
            Console.Write(deal.FrommanagementId);
            Console.Write(" ");
            
            Console.Write(deal.Cost);
            Console.Write(" ");
            
            Console.Write(deal.Status);
            Console.Write(" ");
            
            Console.WriteLine();
        }
    }
}