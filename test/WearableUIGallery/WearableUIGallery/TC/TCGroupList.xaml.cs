﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCGroupList : CirclePage
    {
        public TCGroupList()
        {
            InitializeComponent();
        }
    }

    public class GroupModel : List<NamedList<string>>
    {
        public GroupModel()
        {
            Add(new NamedList<string>("group1") { "Aaliyah", "Aamir", "Aaralyn ", "Aaron", "Abagail", "Babitha", "Bahuratna", "Bandana", "Bulbul", "Cade", "Caldwell" });
            Add(new NamedList<string>("group2") { "Chandan", "Caster", "Dagan ", "Daulat", "Dag", "Earl", "Ebenzer", "Ellison", "Elizabeth", "Filbert", "Fitzpatrick", "Florian", "Fulton" });
            Add(new NamedList<string>("group3") { "Frazer", "Gabriel", "Gage", "Galen", "Garland", "Gauhar", "Hadden", "Hafiz", "Hakon", "Haleem", "Hank", "Hanuman" });
            Add(new NamedList<string>("group4") { "Jabali ", "Jaimini", "Jayadev", "Jake", "Jayatsena", "Jonathan", "Kamaal", "Jeirk", "Jasper", "Jack", "Mac", "Macy", "Marlon", "Milson" });
        }
    }

    public class NamedList<T> : List<T>
    {
        public NamedList(string name) => Name = name;
        public string Name { get; set; }
    }
}