using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CircularUI;
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