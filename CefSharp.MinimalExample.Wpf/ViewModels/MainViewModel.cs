﻿// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.MinimalExample.Wpf.Mvvm;
using CefSharp.Wpf;
using System.ComponentModel;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf.ViewModels
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set { PropertyChanged.ChangeAndNotify(ref webBrowser, value, () => WebBrowser); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { PropertyChanged.ChangeAndNotify(ref title, value, () => Title); }
        }

        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                Application.Current.MainWindow.Title = "CefSharp.MinimalExample.Wpf - " + Title;
            }
        }
    }
}
