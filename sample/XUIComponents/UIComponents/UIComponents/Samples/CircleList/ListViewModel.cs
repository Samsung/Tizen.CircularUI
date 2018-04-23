using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples.CircleList
{
    public class ListViewModel : INotifyPropertyChanged
    {
        const string SelectAll = "Select all";
        const string DeselectAll = "Deselect all";
        static List<string> _names = new List<string>
        {
            "Aaliyah", "Aamir", "Aaralyn", "Aaron", "Abagail",
            "Babitha", "Bahuratna", "Bandana", "Bulbul", "Cade", "Caldwell",
            "Chandan", "Caster", "Dagan ", "Daulat", "Dag", "Earl", "Ebenzer",
            "Ellison", "Elizabeth", "Filbert", "Fitzpatrick", "Florian", "Fulton",
            "Frazer", "Gabriel", "Gage", "Galen", "Garland", "Gauhar", "Hadden",
            "Hafiz", "Hakon", "Haleem", "Hank", "Hanuman", "Jabali ", "Jaimini",
            "Jayadev", "Jake", "Jayatsena", "Jonathan", "Kamaal", "Jeirk",
            "Jasper", "Jack", "Mac", "Macy", "Marlon", "Milson"
        };

        static List<string> _longTexts = new List<string>
        {
            "Hey John, how have you been?",
            "Andy, it's been a long time, how are you man?",
            "I finally have some free time. I just finished taking a big examination, and I'm so relieved that I'm done with it",
            "Wow. How long has it been? It seems like more than a year. I'm doing pretty well. How about you?",
            "I'm playing a video game on my computer because I have nothing to do.",
            "I'm pretty busy right now. I'm doing my homework because I have an exam tomorrow.",
            "I'm taking the day off from work today because I have so many errands. I'm going to the post office to send some packages to my friends."
        };

        int _checkedNamesCount;
        string _selectOptionMessage1;
        string _selectOptionMessage2;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Names => _names;
        public List<string> LongTexts => _longTexts;
        public ObservableCollection<CheckableName> CheckableNames { get; private set; }

        public int CheckedNamesCount
        {
            get => _checkedNamesCount;
            private set
            {
                if (_checkedNamesCount != value)
                {
                    _checkedNamesCount = value;
                    OnPropertyChanged();

                    UpdateSelectOptionMessage();
                }
            }
        }

        public string SelectOptionMessage1
        {
            get => _selectOptionMessage1;
            set
            {
                if (_selectOptionMessage1 != value)
                {
                    _selectOptionMessage1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectOptionMessage2
        {
            get => _selectOptionMessage2;
            set
            {
                if (_selectOptionMessage2 != value)
                {
                    _selectOptionMessage2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SelectCommand1 => new Command(SelectOption1Job);
        public ICommand SelectCommand2 => new Command(SelectOption2Job);



        public ListViewModel()
        {
            CheckableNames = new ObservableCollection<CheckableName>();
            foreach (var name in _names)
            {
                var data = new CheckableName(name, false);
                data.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Checked")
                    {
                        CheckedNamesCount += data.Checked ? 1 : -1;
                    }
                };
                CheckableNames.Add(data);
            }

            UpdateSelectOptionMessage();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void SelectOption1Job()
        {
            bool r = CheckedNamesCount < CheckableNames.Count;
            foreach (var x in CheckableNames)
            {
                x.Checked = r;
            }
        }
        void SelectOption2Job()
        {
            if (CheckedNamesCount > 0 && CheckedNamesCount != CheckableNames.Count)
                foreach (var x in CheckableNames) x.Checked = false;
        }

        void UpdateSelectOptionMessage()
        {
            SelectOptionMessage1 = _checkedNamesCount < CheckableNames.Count ? SelectAll : DeselectAll;
            SelectOptionMessage2 = _checkedNamesCount != 0 && _checkedNamesCount != CheckableNames.Count ? DeselectAll : "";
        }
    }

    public class CheckableName : INotifyPropertyChanged
    {
        string _name;
        bool _checked;

        public CheckableName(string name, bool isChecked)
        {
            _name = name;
            _checked = isChecked;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



    public class MyGroup : List<CheckableName>
    {
        public string GroupName { get; set; }

        public MyGroup(string name) { GroupName = name; }
    }

    public class ListVieGroupModel
    {
        public List<MyGroup> GroupList { get; private set; }

        public ListVieGroupModel()
        {
            GroupList = new List<MyGroup>
            {
                new MyGroup("group1") { new CheckableName("Aaliyah", false), new CheckableName("Aamir", false), new CheckableName("Aaralyn ", false), new CheckableName("Aaron", false), new CheckableName("Abagail", false), new CheckableName("Babitha", false), new CheckableName("Bahuratna", false), new CheckableName("Bandana", false), new CheckableName("Bulbul", false), new CheckableName("Cade", false), new CheckableName("Caldwell", false)},
                new MyGroup("group2") { new CheckableName("Chandan", false), new CheckableName("Caster", false), new CheckableName("Dagan ", false), new CheckableName("Daulat", false), new CheckableName("Dag", false), new CheckableName("Earl", false), new CheckableName("Ebenzer", false), new CheckableName("Ellison", false), new CheckableName("Elizabeth", false), new CheckableName("Filbert", false), new CheckableName("Fitzpatrick", false), new CheckableName("Florian", false), new CheckableName("Fulton", false)},
                new MyGroup("group3") { new CheckableName("Frazer", false), new CheckableName("Gabriel", false), new CheckableName("Gage", false), new CheckableName("Galen", false), new CheckableName("Garland", false), new CheckableName("Gauhar", false), new CheckableName("Hadden", false), new CheckableName("Hafiz", false), new CheckableName("Hakon", false), new CheckableName("Haleem", false), new CheckableName("Hank", false), new CheckableName("Hanuman", false)},
                new MyGroup("group4") { new CheckableName("Jabali", false), new CheckableName("Jaimini", false), new CheckableName("Jayadev", false), new CheckableName("Jake", false), new CheckableName("Jayatsena", false), new CheckableName("Jonathan", false), new CheckableName("Jeirk", false), new CheckableName("Jasper", false), new CheckableName("Jack", false), new CheckableName("Kamaal", false), new CheckableName("Mac", false), new CheckableName("Macy", false), new CheckableName("Marlon", false), new CheckableName("Milson", false)},
            };
        }
    }
}