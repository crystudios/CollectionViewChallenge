﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CollectionViewChallenge.Models;
using CollectionViewChallenge.Controls;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CollectionViewChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewChallengePage : ContentPage
    {
        public ObservableCollection<CustomButton> Buttons { get; set; }

        public CollectionViewChallengePage()
        {
            InitializeComponent();
            Buttons = new ObservableCollection<CustomButton>();
            //temp
            FillButtonList();
        }

        private void FillButtonList()
        {
            var assembly = typeof(CollectionViewChallengePage).GetTypeInfo().Assembly;
            var exp = $"{ assembly.GetName().Name}.sfx.json";
            Stream stream = assembly.GetManifestResourceStream(exp);

            using (var reader = new System.IO.StreamReader(stream))
            {

                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<SfxItems>(json);

                data.sfx_items = data.sfx_items.OrderByDescending(x => x.likeCount).ToList();

                foreach (SfxItem item in data.sfx_items)
                {
                    var btn = new CustomButton(item);
                    Buttons.Add(btn);
                }
            }

            /*var btnAlert = new CustomButton(new SfxItem
            {
                id = 0,
                name = "Button 1",
                category = 1,
                file = "file1",
                enabled = true,
                likeCount = 1
            });
            var btnAnimal = new CustomButton(new SfxItem
            {
                id = 0,
                name = "Button 2",
                category = 2,
                file = "file1",
                enabled = true,
                likeCount = 1
            });
            var btnMeme = new CustomButton(new SfxItem
            {
                id = 0,
                name = "Button 3",
                category = 3,
                file = "file1",
                enabled = true,
                likeCount = 1
            });
            var btnMusic = new CustomButton(new SfxItem
            {
                id = 0,
                name = "Button 4",
                category = 4,
                file = "file1",
                enabled = true,
                likeCount = 1
            });
            var btnSfx = new CustomButton(new SfxItem
            {
                id = 0,
                name = "Button 5",
                category = 5,
                file = "file1",
                enabled = true,
                likeCount = 1
            });

            Buttons.Add(btnAlert);
            Buttons.Add(btnAnimal);
            Buttons.Add(btnMeme);
            Buttons.Add(btnMusic);
            Buttons.Add(btnSfx);*/

            grvButtons.ItemsSource = Buttons;
        }
    }
}