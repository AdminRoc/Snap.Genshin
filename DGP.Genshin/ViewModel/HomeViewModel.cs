﻿using DGP.Genshin.Control;
using DGP.Genshin.Control.GenshinElement;
using DGP.Genshin.Helper;
using DGP.Genshin.MiHoYoAPI.Announcement;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Snap.Core.DependencyInjection;
using Snap.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DGP.Genshin.ViewModel
{
    [ViewModel(InjectAs.Transient)]
    public class HomeViewModel : ObservableObject
    {
        private AnnouncementWrapper? announcement;
        private bool isOpeningUI;

        public AnnouncementWrapper? Announcement
        {
            get => announcement;

            set => SetProperty(ref announcement, value);
        }
        public bool IsOpeningUI
        {
            get => isOpeningUI;

            set => SetProperty(ref isOpeningUI, value);
        }
        public ICommand OpenUICommand { get; }
        public ICommand OpenAnnouncementUICommand { get; }

        public HomeViewModel()
        {
            OpenUICommand = new AsyncRelayCommand(OpenUIAsync);
            OpenAnnouncementUICommand = new RelayCommand<string>(OpenAnnouncementUI);
        }

        private async Task OpenUIAsync()
        {
            IsOpeningUI = true;
            AnnouncementProvider provider = new();
            AnnouncementWrapper? wrapper = await provider.GetAnnouncementWrapperAsync();
            List<AnnouncementContent> contents = new();
            try
            {
                contents = await provider.GetAnnouncementContentsAsync();
            }
            catch (Exception ex)
            {
                this.Log(ex.Message);
            }
            Dictionary<int, string?> contentMap = contents.ToDictionary(id => id.AnnId, iContent => iContent.Content);
            if (wrapper is not null)
            {
                if (wrapper.List is List<AnnouncementListWrapper> announcementListWrappers)
                {
                    //将活动公告置于上方
                    announcementListWrappers.Reverse();
                    //匹配特殊的时间格式
                    Regex timeTagRegrex = new("&lt;t.*?&gt;(.*?)&lt;/t&gt;", RegexOptions.Multiline);
                    Regex timeTagInnerRegex = new("(?<=&lt;t.*?&gt;)(.*?)(?=&lt;/t&gt;)");

                    announcementListWrappers.ForEach(listWrapper =>
                    {
                        listWrapper.List?.ForEach(item =>
                        {
                            string? rawContent = contentMap[item.AnnId];
                            if (rawContent != null)
                            {
                                rawContent = timeTagRegrex.Replace(rawContent, x => timeTagInnerRegex.Match(x.Value).Value);
                            }
                            item.Content = rawContent;
                            item.OpenAnnouncementUICommand = OpenAnnouncementUICommand;
                        });
                    });

                    if (announcementListWrappers[0].List is List<Announcement> activities)
                    {
                        //Match d+/d+/d+ d+:d+:d+ time format
                        Regex dateTimeRegex = new(@"(\d+\/\d+\/\d+\s\d+:\d+:\d+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        activities.ForEach(item =>
                        {
                            Match matched = dateTimeRegex.Match(item.Content ?? "");
                            if (matched.Success && DateTime.TryParse(matched.Value, out DateTime time))
                            {
                                if (time > item.StartTime && time < item.EndTime)
                                {
                                    item.StartTime = time;
                                }
                            }
                        });

                        wrapper.List[0].List = activities
                            .OrderBy(i => i.StartTime)
                            .ThenBy(i => i.EndTime)
                            .ToList();
                    }

                    Announcement = wrapper;
                }
            }
            IsOpeningUI = false;
        }
        private void OpenAnnouncementUI(string? content)
        {
            if (WebView2Helper.IsSupported)
            {
                using (AnnouncementWindow? window = new(content))
                {
                    window.ShowDialog();
                }
            }
            else
            {
                new WebView2RuntimeWindow().ShowDialog();
            }
        }
    }
}
