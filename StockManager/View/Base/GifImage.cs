﻿using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;

namespace StockManager
{
    public static class ExtensionMethods
    {
        public static bool Contains(this string[] stringList, string item)
        {
            foreach (string s in stringList)
                if (item.Equals(s))
                    return true;
            return false;
        }
    }
    public class GifImage : Image
    {
        public static readonly DependencyProperty AllowClickToPauseProperty =
            DependencyProperty.Register("AllowClickToPause", typeof(bool), typeof(GifImage),
                                        new UIPropertyMetadata(true));

        public static readonly DependencyProperty GIFSourceProperty =
            DependencyProperty.Register("GIFSource", typeof(string), typeof(GifImage),
                                        new UIPropertyMetadata("", GIFSource_Changed));

        public static readonly DependencyProperty PlayAnimationProperty =
            DependencyProperty.Register("PlayAnimation", typeof(bool), typeof(GifImage),
                                        new UIPropertyMetadata(true, PlayAnimation_Changed));

        private Bitmap _Bitmap;

        private bool _mouseClickStarted;

        public GifImage()
        {
            MouseLeftButtonDown += GIFImageControl_MouseLeftButtonDown;
            MouseLeftButtonUp += GIFImageControl_MouseLeftButtonUp;
            MouseLeave += GIFImageControl_MouseLeave;
            Click += GIFImageControl_Click;
        }

        public bool AllowClickToPause
        {
            get { return (bool)GetValue(AllowClickToPauseProperty); }
            set { SetValue(AllowClickToPauseProperty, value); }
        }

        public bool PlayAnimation
        {
            get { return (bool)GetValue(PlayAnimationProperty); }
            set { SetValue(PlayAnimationProperty, value); }
        }

        public string GIFSource
        {
            get { return (string)GetValue(GIFSourceProperty); }
            set { SetValue(GIFSourceProperty, value); }
        }

        private void GIFImageControl_Click(object sender, RoutedEventArgs e)
        {
            if (AllowClickToPause)
                PlayAnimation = !PlayAnimation;
        }

        private void GIFImageControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _mouseClickStarted = false;
        }

        private void GIFImageControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_mouseClickStarted)
                FireClickEvent(sender, e);
            _mouseClickStarted = false;
        }

        private void GIFImageControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mouseClickStarted = true;
        }

        private void FireClickEvent(object sender, RoutedEventArgs e)
        {
            if (null != Click)
                Click(sender, e);
        }

        public event RoutedEventHandler Click;

        private static void PlayAnimation_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gic = (GifImage)d;
            if ((bool)e.NewValue)
            {
                //StartAnimation if GIFSource is properly set
                if (null != gic._Bitmap)
                    ImageAnimator.Animate(gic._Bitmap, gic.OnFrameChanged);
            }
            else
                //Pause Animation
                ImageAnimator.StopAnimate(gic._Bitmap, gic.OnFrameChanged);
        }


        private void SetImageGIFSource()
        {
            if (null != _Bitmap)
            {
                ImageAnimator.StopAnimate(_Bitmap, OnFrameChanged);
                _Bitmap = null;
            }
            if (String.IsNullOrEmpty(GIFSource))
            {
                //Turn off if GIF set to null or empty
                Source = null;
                InvalidateVisual();
                return;
            }
            if (File.Exists(GIFSource))
                _Bitmap = (Bitmap)System.Drawing.Image.FromFile(GIFSource);
            else
            {
                //My code
                bool isImageFound = false;
                try
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" + GIFSource, UriKind.Absolute);
                    logo.EndInit();
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(logo));
                    var stream = new MemoryStream();
                    encoder.Save(stream);
                    stream.Flush();
                    var image = new System.Drawing.Bitmap(stream);
                    _Bitmap = image;
                    isImageFound = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (!isImageFound)
                {
                    //Support looking for embedded resources
                    Assembly assemblyToSearch = Assembly.GetAssembly(GetType());
                    _Bitmap = GetBitmapResourceFromAssembly(assemblyToSearch);
                    if (null == _Bitmap)
                    {
                        assemblyToSearch = Assembly.GetCallingAssembly();
                        _Bitmap = GetBitmapResourceFromAssembly(assemblyToSearch);
                        if (null == _Bitmap)
                        {
                            assemblyToSearch = Assembly.GetEntryAssembly();
                            _Bitmap = GetBitmapResourceFromAssembly(assemblyToSearch);
                            if (null == _Bitmap)
                                throw new FileNotFoundException("GIF Source was not found.", GIFSource);
                        }
                    }
                }
            }
            if (PlayAnimation)
                ImageAnimator.Animate(_Bitmap, OnFrameChanged);
        }

        private Bitmap GetBitmapResourceFromAssembly(Assembly assemblyToSearch)
        {
            string[] resourselist = assemblyToSearch.GetManifestResourceNames();
            if (null != assemblyToSearch.FullName)
            {
                string searchName = String.Format("{0}.{1}", assemblyToSearch.FullName.Split(',')[0], GIFSource);
                if (resourselist.Contains(searchName))
                {
                    Stream bitmapStream = assemblyToSearch.GetManifestResourceStream(searchName);
                    if (null != bitmapStream)
                        return (Bitmap)System.Drawing.Image.FromStream(bitmapStream);
                }
            }
            return null;
        }

        private static void GIFSource_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GifImage)d).SetImageGIFSource();
        }


        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                   new OnFrameChangedDelegate(OnFrameChangedInMainThread));
        }

        private void OnFrameChangedInMainThread()
        {
            if (PlayAnimation)
            {
                ImageAnimator.UpdateFrames(_Bitmap);
                Source = GetBitmapSource(_Bitmap);
                InvalidateVisual();
            }
        }


        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        private static BitmapSource GetBitmapSource(Bitmap gdiBitmap)
        {
            IntPtr hBitmap = gdiBitmap.GetHbitmap();
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
                                                                              IntPtr.Zero,
                                                                              Int32Rect.Empty,
                                                                              BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hBitmap);
            return bitmapSource;
        }

        #region Nested type: OnFrameChangedDelegate

        private delegate void OnFrameChangedDelegate();

        #endregion
    }
}
