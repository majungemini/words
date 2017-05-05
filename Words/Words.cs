using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words
{
    public class Words
    {
        public string Word { get; set; }
        public string Definition { get; set; }
        

        public Words(string Word, string Definition)
        {
            this.Word = Word;
            this.Definition = Definition;
           
        }

        public Words()
        {

        }

        public override string ToString()
        {
            return $"{Word}, {Definition}";
        }
    }
}
