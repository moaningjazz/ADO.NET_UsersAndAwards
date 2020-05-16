using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAndAwards.Entities
{
    public class User
    {
        private DateTime _dateOfBirth;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth 
        { 
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value < DateTime.Now ? 
                    value : throw new ArgumentException("Дата рождения не может быть больше текущей");
            }
        }
        public int Age 
        {
            get
            {
                //Вычисляем возраст на основе даты рождения
                return (int)DateTime.Now.Subtract(DateOfBirth).TotalDays / 365;
            }
        }

        public override string ToString()
        {
            return string.Format("Id: {1}{0}" +
                    "Name: {2}{0}" +
                    "DateOfBirth: {3}{0}" +
                    "Age: {4}{0}",
                    Environment.NewLine,
                    Id,
                    Name,
                    DateOfBirth,
                    Age);
        }
    }
}
