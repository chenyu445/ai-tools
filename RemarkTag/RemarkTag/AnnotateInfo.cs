using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RemarkTag
{
    public class AnnotateInfo
    {
        public List<OneObject> objects { set; get; }
    }

    public class OneObject {
        public string name { set; get; }
        public List<Point> points { set; get; }
    }
}
