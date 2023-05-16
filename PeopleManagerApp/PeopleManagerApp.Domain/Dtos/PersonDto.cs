using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagerApp.Domain.Dtos
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
