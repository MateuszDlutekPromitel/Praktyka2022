using System;
using System.Collections.Generic;

namespace promitel1.common
{
    public class Company
    {
        public string Name { get; set; }
        public List<Camera> Cameras { get; set; }
    }
    public class Camera
    {
        public string Name { get; set; }
        public string SN { get; set; }
        public string MAC { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
    }
}
