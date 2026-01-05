using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProgOdevFinal.Models
{
    public class TodoItem
    {
        public string Id { get; set; } 
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }

        public string FormattedDate => Date.ToString("dd.MM.yyyy HH:mm");

        public string StatusColor => IsDone ? "#4CAF50" : "Gray";
    }
}