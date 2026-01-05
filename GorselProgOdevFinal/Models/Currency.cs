using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProgOdevFinal.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string Buying { get; set; }
        public string Selling { get; set; }
        public string ChangeRate { get; set; }


        public string DirectionIcon { get; set; }
        public string ColorCode { get; set; }
    }
}