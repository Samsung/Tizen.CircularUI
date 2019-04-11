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

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WearableUIGallery.TC
{
    public class TestLocation : INotifyPropertyChanged
    {
        Position _position;

        public string Address { get; }
        public string Description { get; }

        public Position Position
        {
            get => _position;
            set
            {
                if (!_position.Equals(value))
                {
                    _position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }

        public TestLocation(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class TCMapViewPinItemsViewModel
    {
        int _pinCreatedCount = 0;
        readonly ObservableCollection<TestLocation> _locations;

        public IEnumerable Locations => _locations;

        public ICommand AddLocationCommand { get; }
        public ICommand RemoveLocationCommand { get; }
        public ICommand ClearLocationsCommand { get; }

        public TCMapViewPinItemsViewModel()
        {
            _locations = new ObservableCollection<TestLocation>()
            {
                new TestLocation("185 Greenwich St, New York, NY 10007", "Westfield World Trade Center", new Position(40.711493, -74.011351)),
                new TestLocation("New York, NY 10004", "Statue of Liberty National Monument", new Position(40.689651, -74.045412)),
                new TestLocation("20 W 34th St, New York, NY 10001", "Empire State Building", new Position(40.748368, -73.985560))
            };

            AddLocationCommand = new Command(AddLocation);
            RemoveLocationCommand = new Command(RemoveLocation);
            ClearLocationsCommand = new Command(() => _locations.Clear());
        }

        void AddLocation()
        {
            _locations.Add(NewLocation());
        }

        void RemoveLocation()
        {
            if (_locations.Any())
            {
                _locations.Remove(_locations.First());
            }
        }

        TestLocation NewLocation()
        {
            _pinCreatedCount++;
            return new TestLocation(
                $"New address {_pinCreatedCount}, New York, NY",
                $"New description {_pinCreatedCount}",
                RandomPosition.Next(new Position(40.7157961, -74.0252194), 0.04, 0.04));
        }

    }

    static class RandomPosition
    {
        static Random Random = new Random(Environment.TickCount);

        public static Position Next(Position position, double latitudeRange, double longitudeRange)
        {
            return new Position(
                position.Latitude + (Random.NextDouble() * 2 - 1) * latitudeRange,
                position.Longitude + (Random.NextDouble() * 2 - 1) * longitudeRange);
        }
    }
}
