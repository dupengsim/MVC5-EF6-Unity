using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.VModel
{
    public class MenuView
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string SourcePath { get; set; }
        public int MenuLevel { get; set; }

        public int Sort { get; set; }
    }
}
