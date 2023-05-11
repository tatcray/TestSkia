using System;
using Avalonia.Controls;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using TestSkia;

namespace TestSkia
{
    public class MainWindowViewModel : MainWindow
    {
        private SkiaBitMapWindow SkiaBitMapWindow;
        public void OpenOptions()
        {
            if (SkiaBitMapWindow == null)
            {
                SkiaBitMapWindow = new SkiaBitMapWindow();
                SkiaBitMapWindow.Show();

            }
            else
            {
                if (!SkiaBitMapWindow.IsVisible)
                {
                    SkiaBitMapWindow = null;
                    SkiaBitMapWindow = new SkiaBitMapWindow();
                    SkiaBitMapWindow.Show();
                }
                else
                {
                    SkiaBitMapWindow.Activate();
                }
            }
        }

        public MainWindowViewModel (Window window)
        {
            //RequestingTechnicalSupport window = new RequestingTechnicalSupport();
            //window.Show();
            // this.CloseWindow = ReactiveCommand.Create(() => window.Close());
        }
        public ReactiveCommand<Unit, Unit> CloseWindow { get; set; }
        
        

    }
}