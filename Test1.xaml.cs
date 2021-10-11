using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace StoragePC
{
    /// <summary>
    /// Логика взаимодействия для Test1.xaml
    /// </summary>
    public partial class Test1 : Window
    {
        ApplicationContext db;
        public Test1()
        {
            InitializeComponent();
            DeviceGrid.Columns[0].IsReadOnly = true;
            DeviceGrid.Columns[1].IsReadOnly = true;
            DeviceGrid.Columns[2].IsReadOnly = true;
            DeviceGrid.Columns[3].IsReadOnly = true;

            db = new ApplicationContext();
            db.Devices.Load();
            Reload();
            DeviceGrid.ItemsSource = db.Devices.Local.ToBindingList();
            //this.DataContext = db.Devices.Local.ToBindingList();
        }
        public void Reload()   //обновление
        {
            DeviceGrid.ItemsSource = db.Devices.ToList();
        }
        // добавление DateBase
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DateBase f = new DateBase(new Device());
            if(f.ShowDialog() == true)
            {
                //Device device = f.Device;
                //db.Devices.Add(device);
                //db.SaveChanges();
                Reload();
            }
            else
            {
                try
                {
                    Device device = f.Device;
                    db.Devices.Add(device);
                    db.SaveChanges();
                    Reload();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    Device device = f.Device;
                    db.Devices.Remove(device);
                    db.SaveChanges();
                    MessageBox.Show("Если пусто(заглушка1)");
                    return;
                }
            }
            

        }
        // редактирование DateBaseFull2
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (DeviceGrid.SelectedItem == null) return;
            // получаем выделенный объект
            Device device = DeviceGrid.SelectedItem as Device;

            DateBaseFull2 f = new DateBaseFull2(new Device
            {
                Id = device.Id,
                Description = device.Description,
                Type = device.Type,
                Kabunet = device.Kabunet,
                Number = device.Number
            });

            if (f.ShowDialog() == true)
            {
                // получаем измененный объект
                device = db.Devices.Find(f.Device.Id);
                if (device != null)
                {
                    device.Description = f.Device.Description;
                    device.Type = f.Device.Type;
                    device.Kabunet = f.Device.Kabunet;
                    device.Number = f.Device.Number;
                    db.Entry(device).State = EntityState.Modified;
                    db.SaveChanges();
                    Reload();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (DeviceGrid.SelectedItem == null)
            {
                return;
            }
            try
            {
                // получаем выделенный объект
                Device device = DeviceGrid.SelectedItem as Device;
                db.Devices.Remove(device);
                db.SaveChanges();
                Reload();
            }
            catch (System.ArgumentNullException)
            {
                //Device device = DeviceGrid.SelectedItem as Device;
                MessageBox.Show("Пустая строчка в базе (заглушка2)");
                //db.Devices.Remove(device);
                //db.SaveChanges();
                Reload();
                return;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //// если ни одного объекта не выделено, выходим
            //DeviceGrid.SelectionMode = DeviceGrid.FullRowSelect;
            //// получаем выделенный объект
            //Device device = DeviceGrid.SelectedItem as Device;
            //db.Devices.Remove(device);
            //db.SaveChanges();
        }

        private void tbSearchDock_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (tbSearchDock.Text == "")
            {
                Reload();
             }
            else
            {
                DeviceGrid.ItemsSource = db.Devices.Where(c => c.Description.Contains(tbSearchDock.Text)).ToList();
                //DeviceGrid.ItemsSource = db.Devices.Where(c => c.Number.Include(tbSearchDock.Text)).ToList();   
                //DeviceGrid.ItemsSource = db.Devices.Where(f => f.Type.Contains(tbSearchDock.Text)).ToList();
                //DeviceGrid.ItemsSource = db.Devices.Where(b => b.Kabunet.Contains(tbSearchDock.Text)).ToList();

            }
            
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

