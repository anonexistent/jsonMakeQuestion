using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonQuestion2
{
    internal class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new();

        public Question() { }
        public Question(int id, string txt, List<Answer> ans)
        {
            Id = id;
            Text = txt;
            Answers = ans;
        }

    }
}
