using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Header
  {
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public bool IsRuigee { get; set; }
    public bool IsHardest { get; set; }
    public bool IsFrameone { get; set; }
    public bool IsPushrock { get; set; }
    public bool IsMorefarm { get; set; }
    public bool IsMystery { get; set; }
    public string Filename { get; set; }
  }
}
