using SuggestionBank.Helpers;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SuggestionBank.Droid.DatabasePath))]
namespace SuggestionBank.Droid 
{

    public class DatabasePath : IDBPath
    {
        public DatabasePath()
        {
        }

        public string GetDbPath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "SuggestionDB.db");
        }
    }

}