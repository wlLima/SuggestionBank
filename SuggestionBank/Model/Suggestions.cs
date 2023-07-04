using System;
using System.Collections.Generic;
using System.Text;

namespace SuggestionBank.Model
{
    public class Suggestions
    {
        public int Id { get; set; }
        public string Collaborator { get; set; }
        public string Departament { get; set; }
        public string Suggestion { get; set; }
        public string Justification { get; set; }
    }
}
