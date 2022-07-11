using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promitel1
{
    public class AccessPermision
    {
        public int No { get; set; } = 0;
        public string PlateNo { get; set; } = string.Empty;
        public int Group { get; set; } = 0;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
        public string CardID { get; set; } = string.Empty;
        public bool Selected { get; set; } = false;
    


    }

}
