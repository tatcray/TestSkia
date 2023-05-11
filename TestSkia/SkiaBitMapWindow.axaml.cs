using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using SkiaSharp;

namespace TestSkia;

public partial class SkiaBitMapWindow : Window
{
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private Canvas _myCanvas;
    private TextBlock _fpsDisplay;
    private Stopwatch _stopwatch;
    private int _frameCount;
    public MainWindowViewModel Data;

    private List<SKBitmap> imageList;
    private int currentIndex;
    private Canvas canvas;

    private int _currentImageIndex = 0;
    private DispatcherTimer timer;

    public SkiaBitMapWindow()
    {
        InitializeComponent();
        
        imageList = new List<SKBitmap>
        {
            LoadBitmap("../../../Imgs/frame_00_delay-0.05s-001.png"),
            LoadBitmap("../../../Imgs/frame_01_delay-0.05s-002.png"),
            LoadBitmap("../../../Imgs/frame_02_delay-0.05s-003.png"),
            LoadBitmap("../../../Imgs/frame_03_delay-0.05s-004.png"),
            LoadBitmap("../../../Imgs/frame_04_delay-0.05s-005.png"),
            LoadBitmap("../../../Imgs/frame_05_delay-0.05s-006.png"),
            LoadBitmap("../../../Imgs/frame_06_delay-0.05s-007.png"),
            LoadBitmap("../../../Imgs/frame_07_delay-0.05s-008.png"),
            LoadBitmap("../../../Imgs/frame_08_delay-0.05s-009.png"),
            LoadBitmap("../../../Imgs/frame_09_delay-0.05s-010.png"),
            LoadBitmap("../../../Imgs/frame_10_delay-0.05s-011.png"),
            LoadBitmap("../../../Imgs/frame_11_delay-0.05s-012.png"),
            LoadBitmap("../../../Imgs/frame_12_delay-0.05s-013.png"),
            LoadBitmap("../../../Imgs/frame_13_delay-0.05s-014.png"),
            LoadBitmap("../../../Imgs/frame_14_delay-0.05s-015.png"),
            LoadBitmap("../../../Imgs/frame_15_delay-0.05s-016.png"),
            LoadBitmap("../../../Imgs/frame_16_delay-0.05s-017.png"),
            LoadBitmap("../../../Imgs/frame_17_delay-0.05s-018.png"),
            LoadBitmap("../../../Imgs/frame_18_delay-0.05s-019.png"),
            LoadBitmap("../../../Imgs/frame_19_delay-0.05s-020.png"),
            LoadBitmap("../../../Imgs/frame_20_delay-0.05s-021.png"),
            LoadBitmap("../../../Imgs/frame_21_delay-0.05s-022.png"),
            LoadBitmap("../../../Imgs/frame_22_delay-0.05s-023.png"),
            LoadBitmap("../../../Imgs/frame_23_delay-0.05s-024.png"),
            LoadBitmap("../../../Imgs/frame_24_delay-0.05s-025.png"),
            LoadBitmap("../../../Imgs/frame_25_delay-0.05s-026.png"),
            LoadBitmap("../../../Imgs/frame_26_delay-0.05s-027.png"),
            LoadBitmap("../../../Imgs/frame_27_delay-0.05s-028.png"),
            LoadBitmap("../../../Imgs/frame_28_delay-0.05s-029.png"),
            LoadBitmap("../../../Imgs/frame_29_delay-0.05s-030.png"),
            LoadBitmap("../../../Imgs/frame_30_delay-0.05s-031.png"),
            LoadBitmap("../../../Imgs/frame_31_delay-0.05s-032.png"),
            LoadBitmap("../../../Imgs/frame_32_delay-0.05s-033.png"),
            LoadBitmap("../../../Imgs/frame_33_delay-0.05s-034.png"),
            LoadBitmap("../../../Imgs/frame_34_delay-0.05s-035.png"),
            LoadBitmap("../../../Imgs/frame_35_delay-0.05s-036.png"),
            LoadBitmap("../../../Imgs/frame_36_delay-0.05s-037.png"),
            LoadBitmap("../../../Imgs/frame_37_delay-0.05s-038.png"),
            LoadBitmap("../../../Imgs/frame_38_delay-0.05s-039.png")
        };

        currentIndex = 0;
        canvas = this.FindControl<Canvas>("Canvas");
        
        ShowImageInCanvas(imageList[currentIndex]);
        
        StartImageLoop();


#if DEBUG
        this.AttachDevTools();
#endif
        StartFpsCounter();
        canvas = this.FindControl<Canvas>("Canvas");
        _fpsDisplay = this.FindControl<TextBlock>("FpsDisplay");

    }
    


    private void StartImageLoop()
    {
        // Запустить таймер или выполнить асинхронную операцию, чтобы циклически прогонять картинки
        // В этом примере мы будем использовать простой DispatcherTimer для эмуляции цикла

        var timer = new Avalonia.Threading.DispatcherTimer();
        timer.Interval = System.TimeSpan.FromMicroseconds(0.5);
        timer.Tick += (sender, e) =>
        {
            // Отобразить следующее изображение в Canvas
            currentIndex = (currentIndex + 1) % imageList.Count;
            ShowImageInCanvas(imageList[currentIndex]);
        };
        timer.Start();
    }

    private void ShowImageInCanvas(SKBitmap bitmap)
    {
        // Очистить Canvas от предыдущих элементов
        canvas.Children.Clear();

        // Создать элемент Image и задать его свойства
        var imageStream = new MemoryStream(SKImage.FromBitmap(bitmap).Encode().ToArray());
        Image image = new Image();
        image.Source = new Bitmap(imageStream);
        /*{
            Source = imageList[0],
            Width = bitmap.Width,
            Height = bitmap.Height
        };*/

        // Добавить элемент Image в Canvas
        canvas.Children.Add(image);
    }
    
    private async void StartFpsCounter()    {
        _frameCount = 0;
        _stopwatch = Stopwatch.StartNew();

        while (true)
        {
            await Task.Delay(1000 / 60); // approximating 60fps target
            _frameCount++;

            if (_stopwatch.Elapsed.TotalSeconds >= 1)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _fpsDisplay.Text = $"FPS: {_frameCount}";
                });

                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }
    
    private SKBitmap LoadBitmap(string imagePath)
    {
        using (var stream = System.IO.File.OpenRead(imagePath))
        {
            return SKBitmap.Decode(stream);
        }
    }
}

