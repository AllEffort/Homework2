using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SchoolProject
{
    class DUNP
    {
        //Razlog sto ovde nemamo eventove i handlere i (skoro) sve je javno jeste zato sto globalni objekat vec posmatramo kao sami dom (window objekat), jedini razlog sto je u posebnom fajlu jeste citljivost i laksi rad
        //Mozemo zamisliti da su nam ovo promenljive stanja (states) od main window sto je svakako public

        //Esencialno svi eventi su isti osim koje argumente nose, u nasem slucaju argumenti su dosta slicni, sa jednom ili dve promenljive razlike, tako da imamo jedan event handler i jedan event args, u argumentima koje nam promenljive ne trebaju samo se ne koriste
        public delegate void UpdateUcenikHanlder(object sender, UpdateUcenikArgs e);
        public event UpdateUcenikHanlder NewUcenik;
        public event UpdateUcenikHanlder DeleteUcenik;
        public event UpdateUcenikHanlder UpdateSmer;
        public event UpdateUcenikHanlder UpdateUcenik;

        //Svaki smer ima ucenike koji polazu
        public List<Smer> GlobalObject { get; } = new List<Smer>();
        
        //Citanje sa tekstualnog fajla i pisanje na njemu, odluka o rasporedjivanju informacija u istom i objasnjenje rada se nalazi u README fajlu u ovom repozitorijumu
        public void WriteToDb()
        {
            try
            {
                FileStream fs = new FileStream("maindb.txt", FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);
                foreach (var item in GlobalObject)
                {
                    writer.WriteLine(item.Name);
                    foreach (var elItemo in item.Polazu)
                    {
                        writer.WriteLine($" {elItemo.FullName.Trim()} {elItemo.Avarage.ToString().Trim()}");
                    }
                }
                writer.Close();
                fs.Close();
            }
            catch (IOException)
            {
                System.Windows.MessageBox.Show("Something went wrong when wirting to database, please try again. If this continues contact your software administrator");
            }
        }
        public void ReadFromDb()
        {
            //Resetanje objekta u slucajnu visestrukog refresanja radi ne dodavanja ponovljenih stavki
            GlobalObject.Clear();
            try
            {
                FileStream fs = new FileStream("maindb.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();

                    if (temp.StartsWith(" ") && !string.IsNullOrWhiteSpace(temp))
                    {
                        string[] a = temp.Trim().Split(' ');
                        AddUcenik(GlobalObject.Last().Name, a[0] + " " + a[1], float.Parse(a[2]));
                    }
                    else
                    {
                        AddSmer(temp);
                    }
                }
                reader.Close();
                fs.Close();
            }
            catch (IOException)
            {
                System.Windows.MessageBox.Show("Something went wrong, please try again. If this continues please contact your softwear administrator");
            }
        }

        //Direktno se ticu glavnog objekta (stanja view tree) tako da nisu evntovi vec imamo direktan pristup na njih
        public void AddSmer(string _NEW)
        {
            //Pravljenje novog smera, subskajbanej na potrebne evente i dodavanje u listu smerova
            Smer temp = new Smer(_NEW);
            NewUcenik += temp.HandleAdd;
            DeleteUcenik += temp.HandleDelete;
            UpdateSmer += temp.HandleSelfUpdate;
            UpdateUcenik += temp.HandleUpdateExisting;
            GlobalObject.Add(temp);
        }
        public void DeleteSmer(string _KEYNAME)
        {
            //Nacin pretrazivanja za smer koji je selectovan jeste preko imena kao kkljuca, dodavanje id-a resava problem ponavljanja imena
            foreach(var item in GlobalObject)
            {
                if (item.Name == _KEYNAME)
                {
                    GlobalObject.Remove(item);
                    return;
                }
            }
        }

        //Pokretanje eventova za smerove, ticu se unutrasnih promenljivih i zato su evntovi, prezervacija enkapsulacije
        public void ChangeSmer(string _OLD_NAME, string _NEW_NAME)
        {
            //Ovde change smer sluzi kao event prenosilac koji pokrece event unutar potrebnog smera, opet preko imena smera kao kljuca, a kasnije taj unutrasnji event radi svoj posao
            if (UpdateSmer != null)
            {
                UpdateSmer(this, new UpdateUcenikArgs { Name = _OLD_NAME, StudentName = _NEW_NAME });
            }
        }
        public void AddUcenik(string _SMER_NAME, string _NAME, float _AVARAGE)
        {
            if (NewUcenik != null)
            {
                NewUcenik(this, new UpdateUcenikArgs { Name= _SMER_NAME, StudentName=_NAME, Avarage=_AVARAGE}) ;
            }
        }
        public void RemoveUcenik(string _SMER_NAME, Ucenik _U)
        {
            if (DeleteUcenik != null)
            {
                DeleteUcenik(this, new UpdateUcenikArgs { Name = _SMER_NAME, StudentName = _U.FullName });
            }
        }
        public void UpdateExistingUcenik(string _SMER_NAME, string _OLD_S_NAME, string _NEW_S_NAME, float _OLD_AVARAGE, float _NEW_AVARAGE)
        {
            if (UpdateUcenik != null)
            {
                UpdateUcenik(this, new UpdateUcenikArgs { Name = _SMER_NAME, StudentName = _OLD_S_NAME, NewStudentName = _NEW_S_NAME, Avarage = _OLD_AVARAGE, NewAvarage = _NEW_AVARAGE });
            }
        }


        //Pomocna funkcija citanja imena smera
        public static string clearString(string _TO_CLEAR)
        {
            //Jer pri ispisivanju drveta dodajemo u header broj koliko ih ima, a isti header koristimo kao kljucnu rec (ime) smera u kojem se ucenici nalaze moramo da ga korigujemo (sklanjamo "Broj stavki X")
            //Verujem da ova funkcija treba da stoji u global object jer se kolisti cesto ali je specificna samo za ovu vrstu headera
            string[] temp = _TO_CLEAR.Split();
            string location = "";
            for (int i = 0; i < temp.Length - 3; i++)
            {
                location += $"{temp[i]} ";
            }
            return location.Trim();
        }

    }
}
