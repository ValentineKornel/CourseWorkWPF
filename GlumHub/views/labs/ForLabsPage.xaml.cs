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

namespace GlumHub
{
    /// <summary>
    /// Логика взаимодействия для ForLabsPage.xaml
    /// </summary>
    public partial class ForLabsPage : Page
    {
        public ForLabsPage()
        {
            InitializeComponent();
            menuSelector.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioButton_Click));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Phone phone = (Phone)this.Resources["iPhone6s"]; // получаем ресурс по ключу
            MessageBox.Show(phone.Price.ToString());
        }

        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBlock1.Text = textBlock1.Text + "sender: " + sender.ToString() + "\n";
            textBlock1.Text = textBlock1.Text + "source: " + e.Source.ToString() + "\n\n";
        }

        private void Preview_Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBlock2.Text = textBlock2.Text + "sender: " + sender.ToString() + "\n";
            textBlock2.Text = textBlock2.Text + "source: " + e.Source.ToString() + "\n\n";
        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            textBlock3.Text = "Вы выбрали: " + selectedRadio.Content.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyWindow myWindow = new MyWindow(); // Создаем новый экземпляр MyWindow
            myWindow.Show();
        }
    }
}
