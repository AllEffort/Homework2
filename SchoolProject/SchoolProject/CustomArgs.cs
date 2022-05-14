using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    //Pretpostavljam da smo mogli da imamo vise vrsta Event args-a ali ovde je mogla da se koristi jedna iako ne bas za pohvalu
    //U posebnom fajlu jer ukoliko ih ima vise lakse je za rad sa njima
    public class UpdateUcenikArgs:EventArgs
    {
        public string Name { get; set; }
        public string StudentName;
        public string NewStudentName;
        public float NewAvarage;
        public float Avarage;
    }
    
}
