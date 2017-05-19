using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.VModel
{
    public class MenuTreeView
    {
        public int id { get; set; }
        public int pId { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string SourcePath { get; set; }
        public List<MenuTreeView> children { get; set; }
        public bool open { get; set; } = false;

        public int MenuLevel { get; set; }
        public int Sort { get; set; }
    }
}
