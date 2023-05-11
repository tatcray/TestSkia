using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace TestSkia;

public partial class MainWindow : Window
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
    

    private List<string> _imagePaths = new List<string>
    {
        "../../../Imgs/frame_00_delay-0.05s-001.png",
        "../../../Imgs/frame_01_delay-0.05s-002.png",
        "../../../Imgs/frame_02_delay-0.05s-003.png",
        "../../../Imgs/frame_03_delay-0.05s-004.png",
        "../../../Imgs/frame_04_delay-0.05s-005.png",
        "../../../Imgs/frame_05_delay-0.05s-006.png",
        "../../../Imgs/frame_06_delay-0.05s-007.png",
        "../../../Imgs/frame_07_delay-0.05s-008.png",
        "../../../Imgs/frame_08_delay-0.05s-009.png",
        "../../../Imgs/frame_09_delay-0.05s-010.png",
        "../../../Imgs/frame_10_delay-0.05s-011.png",
        "../../../Imgs/frame_11_delay-0.05s-012.png",
        "../../../Imgs/frame_12_delay-0.05s-013.png",
        "../../../Imgs/frame_13_delay-0.05s-014.png",
        "../../../Imgs/frame_14_delay-0.05s-015.png",
        "../../../Imgs/frame_15_delay-0.05s-016.png",
        "../../../Imgs/frame_16_delay-0.05s-017.png",
        "../../../Imgs/frame_17_delay-0.05s-018.png",
        "../../../Imgs/frame_18_delay-0.05s-019.png",
        "../../../Imgs/frame_19_delay-0.05s-020.png",
        "../../../Imgs/frame_20_delay-0.05s-021.png",
        "../../../Imgs/frame_21_delay-0.05s-022.png",
        "../../../Imgs/frame_22_delay-0.05s-023.png",
        "../../../Imgs/frame_23_delay-0.05s-024.png",
        "../../../Imgs/frame_24_delay-0.05s-025.png",
        "../../../Imgs/frame_25_delay-0.05s-026.png",
        "../../../Imgs/frame_26_delay-0.05s-027.png",
        "../../../Imgs/frame_27_delay-0.05s-028.png",
        "../../../Imgs/frame_28_delay-0.05s-029.png",
        "../../../Imgs/frame_29_delay-0.05s-030.png",
        "../../../Imgs/frame_30_delay-0.05s-031.png",
        "../../../Imgs/frame_31_delay-0.05s-032.png",
        "../../../Imgs/frame_32_delay-0.05s-033.png",
        "../../../Imgs/frame_33_delay-0.05s-034.png",
        "../../../Imgs/frame_34_delay-0.05s-035.png",
        "../../../Imgs/frame_35_delay-0.05s-036.png",
        "../../../Imgs/frame_36_delay-0.05s-037.png",
        "../../../Imgs/frame_37_delay-0.05s-038.png",
        "../../../Imgs/frame_38_delay-0.05s-039.png"
    };

    private int _currentImageIndex = 0;
    private DispatcherTimer timer;

    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        _myCanvas = this.FindControl<Canvas>("MyCanvas");
        _fpsDisplay = this.FindControl<TextBlock>("FpsDisplay");
        StartSlideShow();
        StartFpsCounter();
    }

    private void StartSlideShow()
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(20);
        timer.Tick += onTimerTick;
        timer.Start();
    }

    private void onTimerTick(object sender, EventArgs e)
    {
        _myCanvas.Children.Clear();
        var image = new Image { Source = new Bitmap(_imagePaths[_currentImageIndex]) };
        _myCanvas.Children.Add(image);
        _currentImageIndex = (_currentImageIndex + 1) % _imagePaths.Count;
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
    
    private void OnButtonClicked(object? sender, RoutedEventArgs e)
    {
        if (Data == null)
        {
            Data = new MainWindowViewModel(this);
        }
        Data.OpenOptions();
        
    }
}

