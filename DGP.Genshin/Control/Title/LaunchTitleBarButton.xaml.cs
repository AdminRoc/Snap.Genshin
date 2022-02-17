﻿using DGP.Genshin.ViewModel.Title;
using ModernWpf.Controls.Primitives;

namespace DGP.Genshin.Control.Title
{
    public partial class LaunchTitleBarButton : TitleBarButton
    {
        public LaunchTitleBarButton()
        {
            DataContext = App.AutoWired<LaunchViewModel>();
            InitializeComponent();
        }

        public LaunchViewModel ViewModel => (LaunchViewModel)DataContext;

        private void Flyout_Closed(object sender, object e)
        {
            ViewModel.CloseUICommand.Execute(null);
        }
    }
}
