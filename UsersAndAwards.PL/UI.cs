using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.BLL.Interfaces;
using UsersAndAwards.Ioc;
using Ninject;
using UsersAndAwards.Entities;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace UsersAndAwards.PL
{
    public static class UI
    {
        private static IUserLogic _userLogic = DependencyResolver.Kernel.Get<IUserLogic>();
        private static IAwardLogic _awardLogic = DependencyResolver.Kernel.Get<IAwardLogic>();

        public static void Start()
        {
            int numAction;
            bool menuOn = true;
            while (menuOn)
            {
                Console.WriteLine("1 - Добавить пользователя" + Environment.NewLine +
                    "2 - Удалить пользователя" + Environment.NewLine +
                    "3 - Добавить награду" + Environment.NewLine +
                    "4 - Удалить награду" + Environment.NewLine +
                    "5 - Список пользователей" + Environment.NewLine +
                    "6 - Список наград" + Environment.NewLine +
                    "7 - Вознаградить пользователя" + Environment.NewLine +
                    "8 - Удалить награду у пользователя" + Environment.NewLine +
                    "9 - Показать список пользователей вместе с наградами" + Environment.NewLine +
                    "0 - Выйти");
                if (int.TryParse(Console.ReadLine(), out numAction) &&
                    numAction >= 0 && numAction <= 9)
                {
                    switch (numAction)
                    {
                        case 0:
                            menuOn = false;
                            break;
                        case 1:
                            AddUser();
                            break;
                        case 2:
                            RemoveUser();
                            break;
                        case 3:
                            AddAward();
                            break;
                        case 4:
                            RemoveAward();
                            break;
                        case 5:
                            GetUsers();
                            break;
                        case 6:
                            GetAwards();
                            break;
                        case 7:
                            Reward();
                            break;
                        case 8:
                            UnAward();
                            break;
                        case 9:
                            ShowUsersAndAwards();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверная команда, введите заново.");
                }
            }
        }

        private static void AddUser()
        {
            string name;
            DateTime dateOfBirth;
            Console.WriteLine("Введите имя:");
            name = Console.ReadLine();
            while (name.Length > 50)
            {
                Console.WriteLine("Ошибка, слишком длинное имя! Максимум 50 символов" + Environment.NewLine +
                    "Введите заново: ");
                name = Console.ReadLine();
            }
            Console.WriteLine("Введите дату (гггг/мм/дд):");
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.WriteLine("Ошибка, введен неверный формат!" + Environment.NewLine +
                    "Введите заново: ");
            }

            try
            {
                _userLogic.Add(new User()
                {
                    Name = name,
                    DateOfBirth = dateOfBirth
                });
            }
            catch (SqlTypeException)
            {
                Console.WriteLine("Неверная дата рождения");
            }
            
        }

        private static void GetUsers()
        {
            Console.WriteLine("Список пользователей: ");
            foreach (var user in _userLogic.GetAll())
            {
                Console.WriteLine(user.ToString());
            }
        }

        private static void RemoveUser()
        {
            Console.WriteLine("Введите ID пользователя которого вы хотите удалить:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }
            _userLogic.RemoveById(id);
        }

        private static void AddAward()
        {
            Console.WriteLine("Введите название награды: ");
            string title = Console.ReadLine();
            while (title.Length > 50)
            {
                Console.WriteLine("Ошибка, слишком длинное название (максимум 50 символов), введите ещё раз: ");
                title = Console.ReadLine();
            }
            _awardLogic.Add(new Award()
            {
                Title = title
            });
        }

        private static void GetAwards()
        {
            Console.WriteLine("Список наград: ");
            foreach (var award in _awardLogic.GetAll())
            {
                Console.WriteLine(award.ToString());
            }
        }

        private static void RemoveAward()
        {
            Console.WriteLine("Введите ID награды, которую вы хотите удалить:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }
            _awardLogic.Remove(id);
        }

        private static void Reward()
        {
            Console.WriteLine("Введите ID пользователя:");
            int idUser;
            while (!int.TryParse(Console.ReadLine(), out idUser))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }

            Console.WriteLine("Введите ID награды:");
            int idAward;
            while (!int.TryParse(Console.ReadLine(), out idAward))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }
            try
            {
                _awardLogic.Reward(new User() { Id = idUser }, new Award() { Id = idAward });
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка, указанной награды или пользователя не существует!");
            }
        }

        private static void UnAward()
        {
            Console.WriteLine("Введите ID пользователя:");
            int idUser;
            
            while (!int.TryParse(Console.ReadLine(), out idUser))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }

            try
            {
                Console.WriteLine("Пользователь: {1}{0}", _userLogic.GetById(idUser).ToString(), Environment.NewLine);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Указанный пользователь не существует!");
                return;
            }


            var awards = _awardLogic.GetUserAwards(idUser);
            if (awards.Count() > 0)
            {
                Console.WriteLine("Список его наград:");
                foreach (Award award in awards)
                {
                    Console.WriteLine(award.ToString());
                }
            }
            else
            {
                Console.WriteLine("У пользователя нет наград!");
                return;
            }

            Console.WriteLine("Введите ID награды:");
            int idAward;
            while (!int.TryParse(Console.ReadLine(), out idAward))
            {
                Console.WriteLine("Ошибка, введенно не целое число");
            }
            try
            {
                _awardLogic.UnReward(new User() { Id = idUser }, new Award() { Id = idAward });
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка, указанной награды не существует у пользователя!");
                Reward();
            }
        }

        private static void ShowUsersAndAwards()
        {
            foreach(User user in _userLogic.GetAll())
            {
                Console.WriteLine("Пользователь: {0}", user.ToString());
                var awards = _awardLogic.GetUserAwards(user.Id);
                if (awards.Count() > 0)
                {
                    Console.WriteLine("Список наград: ");
                    foreach (Award award in _awardLogic.GetUserAwards(user.Id))
                    {
                        Console.WriteLine(award.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("У пользователя нет наград");
                }
            }
        }
    }
}
