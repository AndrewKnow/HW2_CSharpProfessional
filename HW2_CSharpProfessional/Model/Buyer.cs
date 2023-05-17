using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_CSharpProfessional.Model
{
    public class Buyer
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public int ProductId { get; set; } = 0;
        public int Count { get; set; } = 0;    
    }
}
