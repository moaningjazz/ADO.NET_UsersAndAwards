using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAndAwards.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {1}{0}" +
                    "Title: {2}{0}",
                    Environment.NewLine,
                    Id,
                    Title);
        }
    }
}
