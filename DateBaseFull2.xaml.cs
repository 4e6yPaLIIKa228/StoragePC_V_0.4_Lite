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
using System.Windows.Shapes;
using System.Data.Entity;

namespace StoragePC
{
    /// <summary>
    /// Логика взаимодействия для DateBaseFull2.xaml
    /// </summary>
    public partial class DateBaseFull2 : Window
    {
        ApplicationContext db;
        public Device Device { get; private set; }

        public DateBaseFull2(Device p)
        {
            InitializeComponent();
            db = new ApplicationContext();
            Device = p;
            this.DataContext = Device;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(TexBxName.Text) || String.IsNullOrEmpty(TexBxNumber.Text) || String.IsNullOrEmpty(TexBxtype.Text) || String.IsNullOrEmpty(TexBxKab.Text)) //Проверка,если ничего не ввели
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //db.Devices.Add(Device);
                //db.SaveChanges();

                try
                {

                    db.Devices.Add(Device);
                    db.SaveChanges();
                    this.DialogResult = true;

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {

                    //db.Devices.Remove(Device);
                    //db.SaveChanges();
                    //MessageBox.Show("Такой номер уже занят,выберите другой2");
                    this.DialogResult = true;
                    return;

                }
            }

            //this.DialogResult = true;

        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /* e.Handled = "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХФЫВАПРОЛДЖЭЯЧСМИТБЮ".IndexOf(e.Text) < 0;*/ //Только буквы(очень спорно)
        }
    }

}
