using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SchoolProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Inicijalizacija globalnog ubjekta
        DUNP DOM = new DUNP();
        public MainWindow()
        {
            InitializeComponent();
           
        }
        //Ucitavanje baze prilikom pokretanja
        private void StudijskiProgrami_Loaded(object sender, RoutedEventArgs e)
        {
            StudijskiProgrami.SelectedItemChanged += WriteCurrent;
            DOM.ReadFromDb();
            MakeTree();
        }

        //Dodavanje, brisanje, menjanje,
        private void ImportNew(object sender, RoutedEventArgs e)
        {
            if (MainInput.Text == "")
            {
                MessageBox.Show("Molimo unesite vrednost unosa :D");
                return;
            };
            if (!(StudijskiProgrami.SelectedItem is TreeViewItem))
            {
                DOM.AddSmer(MainInput.Text);
            }
            else
            {
                if (Smer.CheckForUcenikInput(MainInput.Text)!=null)
                {
                   DOM.AddUcenik(DUNP.clearString((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString().Trim()), Smer.CheckForUcenikInput(MainInput.Text).FullName, Smer.CheckForUcenikInput(MainInput.Text).Avarage);
                }
                else
                {
                    MessageBox.Show("Neispravan unos molim unesite ime prezime prosek: (Dejan Aksovic 40)");
                }
            }
            MakeTree();
            ClearInput();
        }
        private void DeleteOld_Click(object sender, RoutedEventArgs e)
        {
            if (StudijskiProgrami.SelectedValue != null) {
                if ((StudijskiProgrami.SelectedItem as TreeViewItem).Parent is TreeView)
                {

                    DOM.DeleteSmer(DUNP.clearString((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString().Trim()));
                }
            else
                {

                    DOM.RemoveUcenik(DUNP.clearString(((StudijskiProgrami.SelectedItem as TreeViewItem).Parent as TreeViewItem).Header.ToString()), Smer.CheckForUcenikInput((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString()));
                }
                ClearInput();
                MakeTree();
            }
            else
            {
                MessageBox.Show("Molimo oznacite stavku <3");
            }
            ClearInput();
        }
        public void UpdateExisting_Click(object sender, RoutedEventArgs e)
        {
            if (StudijskiProgrami.SelectedValue != null & !string.IsNullOrWhiteSpace(MainInput.Text))
            {
                if ((StudijskiProgrami.SelectedItem as TreeViewItem).Parent is TreeView)
                {
                    DOM.ChangeSmer(DUNP.clearString((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString()), MainInput.Text);
                }
                else
                {
                    if (Smer.CheckForUcenikInput(MainInput.Text) != null)
                    {
                        Ucenik temp = Smer.CheckForUcenikInput(MainInput.Text);
                        Ucenik oldTemp = Smer.CheckForUcenikInput((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString());
                        string SmerName = DUNP.clearString(((StudijskiProgrami.SelectedItem as TreeViewItem).Parent as TreeViewItem).Header.ToString());
                        DOM.UpdateExistingUcenik(SmerName, oldTemp.FullName, temp.FullName, oldTemp.Avarage, temp.Avarage);
                    }
                    else
                    {
                        MessageBox.Show("Neispravan unos ucenika, molim unesite ime prezime prosek pr: Dejan Aksovic 10");
                    }
                }
                MakeTree();
            }
            else
            {
                MessageBox.Show("Molimo oznacite vrednost za update ili unesite neku vrednost :^ )");
            }
            ClearInput();
        }

        //Upisivanje (updatovanje) i citanje (refreshovanje) sa baze
        private void Write_Click(object sender, RoutedEventArgs e)
        {
            DOM.WriteToDb();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DOM.ReadFromDb();
            MakeTree();
        }

        //Pomocne funkcije za ispis formitanje drveta na osnovu globalnog objekta i praznjenje inputa
        public void WriteCurrent(object sender, RoutedEventArgs e)
        {
            if (StudijskiProgrami.SelectedValue != null)
            {
                if((StudijskiProgrami.SelectedValue as TreeViewItem).Parent is TreeView)
                {
                    Current.Text = DUNP.clearString((StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString());
                }
                else
                {
                    Current.Text = (StudijskiProgrami.SelectedItem as TreeViewItem).Header.ToString();
                }

            }
        }
        private void ClearInput()
        {
            MainInput.Text = "";
        }
        private void MakeTree()
        {
            //reset
            StudijskiProgrami.Items.Clear();

            foreach (var item in DOM.GlobalObject)
            {
                TreeViewItem temp = new TreeViewItem();
                temp.Header = $"{item.Name} Broj stavki: {item.Polazu.Count}";
                StudijskiProgrami.Items.Add(temp);
                foreach (var item2 in item.Polazu)
                {
                    TreeViewItem temp2 = new TreeViewItem();
                    temp2.Header = $"{item2.FullName} {item2.Avarage.ToString()}";
                    temp.Items.Add(temp2);
                }
            }
        }
    }
}
